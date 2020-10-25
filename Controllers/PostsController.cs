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
    public class PostsController : ControllerBase
    {

        private readonly IPostRepository _psRepo;
        private readonly IMapper _mapper;

        public PostsController(IPostRepository PostRepo, IMapper mapper)
        {

            _psRepo = PostRepo;
            _mapper = mapper;

        }


        /// <summary>
        /// Fetches a list of all Posts.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Resident")]
        [ProducesResponseType(200, Type = typeof(List<PostUpdateDto>))]
        public IActionResult GetPosts()
        {

            var objList = _psRepo.GetPosts();

            var objDto = new List<PostUpdateDto>();

            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<PostUpdateDto>(obj));
            }

            return Ok(objDto);

        }

        /// <summary>
        /// Get an individual Post.
        /// </summary>
        /// <param name="PostId">the Id of the Post</param>
        /// <returns></returns>
        [HttpGet("{PostId:int}", Name="GetPost")]
        [ProducesResponseType(200, Type = typeof(PostUpdateDto))]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin,Resident")]
        [ProducesDefaultResponseType]
        public IActionResult GetPost(int PostId)
        {

            var obj = _psRepo.GetPost(PostId);

            if (obj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<PostUpdateDto>(obj);
            return Ok(objDto);

        }

        /// <summary>
        /// Create a new Post.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(PostCreateDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public IActionResult CreatePost([FromBody] PostCreateDto PostDto)
        {

            if (PostDto == null)
            {
                return BadRequest(ModelState);
            }

            var PostObj = _mapper.Map<Post>(PostDto);

            if (!_psRepo.CreatePost(PostObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {PostObj.Id}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetPost", new { version = HttpContext.GetRequestedApiVersion().ToString(),
                                                                            PostId = PostObj.Id }, PostObj);

        }

        /// <summary>
        /// Updates a specific Post.
        /// </summary>
        /// <returns></returns>
        [HttpPatch("{PostId:int}", Name = "UpdatePost")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdatePost(int PostId, [FromBody] PostUpdateDto PostDto)
        {


            if (PostDto == null || PostId != PostDto.Id)
            {
                return BadRequest(ModelState);
            }

            var PostObj = _mapper.Map<Post>(PostDto);

            if (!_psRepo.UpdatePost(PostObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {PostObj.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        /// <summary>
        /// Deletes an Post.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{PostId:int}", Name = "DeletePost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public IActionResult DeletePost(int PostId)
        {

            if (!_psRepo.PostExists(PostId))
            {
                return NotFound();
            }

            var PostObj = _psRepo.GetPost(PostId);

            if (!_psRepo.DeletePost(PostObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {PostObj.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}
