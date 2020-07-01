using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Response;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.Collections.Generic;
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
        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            //La forma empirica, de convertir la respuesta a un DTO
            //var posts =await _postRepository.GetPosts();
            //var postsDTO = posts.Select(x => new PostDTO
            //{
            //    PostId = x.PostId,
            //    Date = x.Date,
            //    Description = x.Description,
            //    Image = x.Image,
            //    UserId = x.UserId

            //});
            //return Ok(postsDTO);

            //La forma correcta de pasar con AutoMapper
            var posts = await _postService.GetPosts();
            //Usando Automapper
            var postDTOs = _mapper.Map<IEnumerable<PostDTO>>(posts);
            //IMplementando la clase generica para los Resposne.
            var response = new APIResponse<IEnumerable<PostDTO>>(postDTOs);
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
}
