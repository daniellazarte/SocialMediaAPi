using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Configuration;
using Newtonsoft.Json;
using SocialMedia.Api.Response;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Interfaces;
using SocialMedia.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        //private readonly IPostRepository _postRepository; // CAmbiado para que trabaje con el Post Service
        private readonly IPostService _postService; // Con ctrl + RR se cambia para todas las cadenas Refactor
        //Inyectar el auto Mapper
        private readonly IMapper _mapper;
        private readonly IUriService _uriservice;
        public PostController(IPostService postService, IMapper mapper, IUriService uriservice)
        {
            _postService = postService;
            _mapper = mapper;
            _uriservice = uriservice;
        }

        [HttpGet(Name = nameof(GetPosts))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetPosts([FromQuery] PostQueryFilter filters) //Impotante el fromquery
        {
            //La forma correcta de pasar con AutoMapper
            var posts = _postService.GetPosts(filters);
            //Usando Automapper
            var postDTOs = _mapper.Map<IEnumerable<PostDTO>>(posts);

            //Guardar en el header valores de paginacion y en la respuesta
            var metadata = new Metadata
            {
                TotalCount = posts.TotalCount,
                PageSize = posts.PageSize,
                CurrentPage = posts.CurrentPage,
                Totalpages = posts.TotalPages,
                HasNexttPage = posts.HasNextPage,
                HasPreviousPage = posts.HasPreviousPage,
                NextPageUrl = _uriservice.GetPostPaginationURI(filters, Url.RouteUrl(nameof(GetPosts))).ToString(),
                PreviousPageUrl = _uriservice.GetPostPaginationURI(filters, Url.RouteUrl(nameof(GetPosts))).ToString()

            };

            //IMplementando la clase generica para los Resposne.
            var response = new APIResponse<IEnumerable<PostDTO>>(postDTOs) { Meta = metadata };

            Response.Headers.Add("x-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postService.GetPost(id);
            //Usando Automapper
            var postDTO = _mapper.Map<PostDTO>(post);
            //var postDTO  = new PostDTO
            //{
            //    PostId = post.PostId,
            //    Date = post.Date,
            //    Description = post.Description,
            //    Image = post.Image,
            //    UserId = post.UserId
            //};

            //Implementando el API Response
            var response = new APIResponse<PostDTO>(postDTO);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostDTO postDTO)
        {
            //var post = new Post
            //{
            //    Date = postDTO.Date,
            //    Description = postDTO.Description,
            //    Image = postDTO.Image,
            //    UserId = postDTO.UserId

            //};
            
            //Usando Automapper
            var post = _mapper.Map<Post>(postDTO);
            await _postService.InsertarPost(post);

            //Convertirlo nuevamente para devolver DTO con el nuevo ID
            postDTO = _mapper.Map<PostDTO>(post);
            var response = new APIResponse<PostDTO>(postDTO);


            return Ok(response);
        }

        //Atualizar un Nuevo Recurso POST HTTPPut
        [HttpPut]
        public async Task<IActionResult> Put(int id, PostDTO postDTO)
        {

            //Actualizar un Post con el verbo HTTPPut
            var post = _mapper.Map<Post>(postDTO);
            post.id = id;

            var result = await _postService.UpdatePost(post);
            
            //Implementando el APIResponse
            var response = new APIResponse<bool>(result);
            return Ok(response);

        }

        //Eliminar un Recurso
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            //Actualizar un Post con el verbo HTTPPut
            //var post = _mapper.Map<Post>(postDTO); // NO Se requiere Mapeo
            var result = await _postService.DeletePost(id);
            //Implementando el APIResponse
            var response = new APIResponse<bool>(result);
            return Ok(response);
        }
    }

    internal interface IUriservice
    {
    }
}
