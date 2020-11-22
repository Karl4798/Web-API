﻿using System.Collections.Generic;
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
    public class ApplicationsController : ControllerBase
    {
        // Injected dependencies
        private readonly IApplicationRepository _apRepo;
        private readonly IMapper _mapper;

        // Constructor
        public ApplicationsController(IApplicationRepository applicationRepo, IMapper mapper)
        {
            _apRepo = applicationRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Fetches a list of all applications.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(List<ApplicationUpdateDto>))]
        public IActionResult GetApplications()
        {
            var objList = _apRepo.GetApplications();
            var objDto = new List<ApplicationUpdateDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<ApplicationUpdateDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get an individual application.
        /// </summary>
        /// <param name="ApplicationId">The Id of the application</param>
        /// <returns></returns>
        [HttpGet("{ApplicationId:int}", Name="GetApplication")]
        [ProducesResponseType(200, Type = typeof(ApplicationUpdateDto))]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]
        [ProducesDefaultResponseType]
        public IActionResult GetApplication(int ApplicationId)
        {
            var obj = _apRepo.GetApplication(ApplicationId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<ApplicationUpdateDto>(obj);
            return Ok(objDto);
        }

        /// <summary>
        /// Create a new application.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ApplicationCreateDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateApplication([FromBody] ApplicationCreateDto ApplicationDto)
        {
            if (ApplicationDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_apRepo.ApplicationExists(ApplicationDto.IdentityNumber))
            {
                ModelState.AddModelError("", "application Exists!");
                return StatusCode(404, ModelState);
            }
            var ApplicationObj = _mapper.Map<Application>(ApplicationDto);
            if (!_apRepo.CreateApplication(ApplicationObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {ApplicationObj.IdentityNumber}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetApplication", new { version = HttpContext.GetRequestedApiVersion().ToString(),
                                                                            ApplicationId = ApplicationObj.Id }, ApplicationObj);
        }

        /// <summary>
        /// Updates a specific application.
        /// </summary>
        /// <returns></returns>
        [HttpPatch("{ApplicationId:int}", Name = "UpdateApplication")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateApplication(int ApplicationId, [FromBody] ApplicationUpdateDto ApplicationDto)
        {
            if (ApplicationDto == null || ApplicationId != ApplicationDto.Id)
            {
                return BadRequest(ModelState);
            }
            var ApplicationObj = _mapper.Map<Application>(ApplicationDto);
            if (!_apRepo.UpdateApplication(ApplicationObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {ApplicationObj.IdentityNumber}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        /// <summary>
        /// Deletes a specific application.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{ApplicationId:int}", Name = "DeleteApplication")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteApplication(int ApplicationId)
        {
            if (!_apRepo.ApplicationExists(ApplicationId))
            {
                return NotFound();
            }
            var ApplicationObj = _apRepo.GetApplication(ApplicationId);
            if (!_apRepo.DeleteApplication(ApplicationObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {ApplicationObj.IdentityNumber}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}