using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IPostRepository _postRepository;
        //Inyectar el auto Mapper
        private readonly IMapper _mapper;
        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
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
            var posts = await _postRepository.GetPosts();
            //Usando Automapper
            var postDTO = _mapper.Map<IEnumerable<PostDTO>>(posts);
            return Ok(postDTO);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postRepository.GetPost(id);
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
            return Ok(postDTO);
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
            await _postRepository.InsertarPost(post);
            return Ok(post);
        }
    }
}
