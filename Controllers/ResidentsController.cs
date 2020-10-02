using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdMedAPI.Models;
using AdMedAPI.Models.Dtos;
using AdMedAPI.Repository.IRepository;

namespace AdMedAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ResidentsController : ControllerBase
    {

        private readonly IResidentRepository _reRepo;
        private readonly IMapper _mapper;

        public ResidentsController(IResidentRepository ResidentRepo, IMapper mapper)
        {

            _reRepo = ResidentRepo;
            _mapper = mapper;

        }


        /// <summary>
        /// Fetches a list of all Residents.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(List<ResidentUpdateDto>))]
        public IActionResult GetResidents()
        {

            var objList = _reRepo.GetResidents();

            var objDto = new List<ResidentUpdateDto>();

            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<ResidentUpdateDto>(obj));
            }

            return Ok(objDto);

        }

        /// <summary>
        /// Get an individual resident.
        /// </summary>
        /// <param name="ResidentId">The Id of the Resident</param>
        /// <returns></returns>
        [HttpGet("{ResidentId:int}", Name="GetResident")]
        [ProducesResponseType(200, Type = typeof(ResidentUpdateDto))]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]
        [ProducesDefaultResponseType]
        public IActionResult GetResident(int ResidentId)
        {

            var obj = _reRepo.GetResident(ResidentId);

            if (obj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<ResidentUpdateDto>(obj);
            return Ok(objDto);

        }

        /// <summary>
        /// Create a new resident.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ResidentCreateDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateResident([FromBody] ResidentCreateDto ResidentDto)
        {

            if (ResidentDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_reRepo.ResidentExists(ResidentDto.IdentityNumber))
            {
                ModelState.AddModelError("", "Resident Exists!");
                return StatusCode(404, ModelState);
            }

            var ResidentObj = _mapper.Map<Resident>(ResidentDto);



            if (!_reRepo.CreateResident(ResidentObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {ResidentObj.IdentityNumber}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetResident", new { version = HttpContext.GetRequestedApiVersion().ToString(),
                                                                            ResidentId = ResidentObj.Id }, ResidentObj);

        }

        /// <summary>
        /// Updates a specific resident.
        /// </summary>
        /// <returns></returns>
        [HttpPatch("{ResidentId:int}", Name = "UpdateResident")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateResident(int ResidentId, [FromBody] ResidentUpdateDto ResidentDto)
        {


            if (ResidentDto == null || ResidentId != ResidentDto.Id)
            {
                return BadRequest(ModelState);
            }

            var ResidentObj = _mapper.Map<Resident>(ResidentDto);

            if (!_reRepo.UpdateResident(ResidentObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {ResidentObj.IdentityNumber}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        /// <summary>
        /// Deletes an resident.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{ResidentId:int}", Name = "DeleteResident")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteResident(int ResidentId)
        {

            if (!_reRepo.ResidentExists(ResidentId))
            {
                return NotFound();
            }

            var ResidentObj = _reRepo.GetResident(ResidentId);

            if (!_reRepo.DeleteResident(ResidentObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {ResidentObj.IdentityNumber}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}
