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
    public class MedicationsController : ControllerBase
    {


        private readonly IMedicationRepository _meRepo;
        private readonly IMapper _mapper;

        public MedicationsController(IMedicationRepository medicationRepo, IMapper mapper)
        {

            _meRepo = medicationRepo;
            _mapper = mapper;

        }


        /// <summary>
        /// Fetches a list of all medications.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(List<MedicationUpdateDto>))]
        public IActionResult GetMedications()
        {

            var objList = _meRepo.GetMedications();

            var objDto = new List<MedicationUpdateDto>();

            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<MedicationUpdateDto>(obj));
            }

            return Ok(objDto);

        }

        /// <summary>
        /// Get an individual medication.
        /// </summary>
        /// <param name="MedicationId">The Id of the medication</param>
        /// <returns></returns>
        [HttpGet("{MedicationId:int}", Name="GetMedication")]
        [ProducesResponseType(200, Type = typeof(MedicationUpdateDto))]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]
        [ProducesDefaultResponseType]
        public IActionResult GetMedication(int MedicationId)
        {

            var obj = _meRepo.GetMedication(MedicationId);

            if (obj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<MedicationUpdateDto>(obj);
            //var objDto = new MedicationDto()
            //{
            //    Created = obj.Created,
            //    Id = obj.Id,
            //    Name = obj.Name,
            //    State = obj.State
            //};
            return Ok(objDto);

        }

        /// <summary>
        /// Create a new medication.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(MedicationCreateDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateMedication([FromBody] MedicationCreateDto MedicationDto)
        {

            if (MedicationDto == null)
            {
                return BadRequest(ModelState);
            }

            var MedicationObj = _mapper.Map<Medication>(MedicationDto);


            if (!_meRepo.CreateMedication(MedicationObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {MedicationObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetMedication", new { version = HttpContext.GetRequestedApiVersion().ToString(),
                                                                            MedicationId = MedicationObj.Id }, MedicationObj);

        }

        /// <summary>
        /// Updates a specific medication.
        /// </summary>
        /// <returns></returns>
        [HttpPatch("{MedicationId:int}", Name = "UpdateMedication")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateMedication(int MedicationId, [FromBody] MedicationUpdateDto MedicationDto)
        {


            if (MedicationDto == null || MedicationId != MedicationDto.Id)
            {
                return BadRequest(ModelState);
            }

            var MedicationObj = _mapper.Map<Medication>(MedicationDto);

            if (!_meRepo.UpdateMedication(MedicationObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {MedicationObj.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        /// <summary>
        /// Deletes an medication.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{MedicationId:int}", Name = "DeleteMedication")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteMedication(int MedicationId)
        {

            var MedicationObj = _meRepo.GetMedication(MedicationId);

            if (!_meRepo.DeleteMedication(MedicationObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {MedicationObj.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}
