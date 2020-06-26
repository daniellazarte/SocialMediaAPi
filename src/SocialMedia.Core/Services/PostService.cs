using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//En esta clase haremnos las reglas de negocio, este servicio se encargara de eso.
namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public PostService(IPostRepository postRepository,IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<Post> GetPost(int id)
        {
            return await _postRepository.GetPost(id);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _postRepository.GetPosts();
        }

        public async Task InsertarPost(Post post)
        {
            //Regla 1 : Validar si el usuario Existe
            //Regla 2: No esta permitido hacer publicaciones que tengan referencia a Sexo
            //Usando la nueva interfaz para validar
            var user = await _userRepository.GetUser(post.UserId);
            if(user == null)
            {
                throw new Exception("User doesnt Exist, Sorry");
            }

            //Regla 2
            if (post.Description.Contains("Sexo"))
            {
                throw new Exception("Content Sex not allowed. ");
            }
            
            await _postRepository.InsertarPost(post);
        }

        public async Task<bool>UpdatePost(Post post)
        {
            return await _postRepository.UpdatePost(post);
        }

        public async Task<bool> DeletePost(int id)
        {
            return await _postRepository.DeletePost(id);
        }
    }
}
