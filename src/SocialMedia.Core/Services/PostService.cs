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
        //private readonly IRepository<Post> _postRepository;
        //private readonly IRepository<User> _userRepository;
        //private readonly IRepository<Comment> _commentRepository;
        //Ahora Usamos el Unit OPf work ya no necesitaremos la dependencia de post ni de user

        private readonly IUnitOfWork _unitOfWork;
        public PostService(IUnitOfWork unitOfWork)
        {
            //_postRepository = postRepository;
            //_userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Post> GetPost(int id)
        {
            return await _unitOfWork.PostRepository.GetById(id);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            //return await _postRepository.GetAll();
            return await _unitOfWork.PostRepository.GetAll();
        }

        public async Task InsertarPost(Post post)
        {
            //Regla 1 : Validar si el usuario Existe
            //Regla 2: No esta permitido hacer publicaciones que tengan referencia a Sexo
            //Usando la nueva interfaz para validar
            var user = await _unitOfWork.UserRepository.GetById(post.UserId);
            if(user == null)
            {
                throw new Exception("User doesnt Exist, Sorry");
            }

            //Regla 2
            if (post.Description.Contains("Sexo"))
            {
                throw new Exception("Content Sex not allowed. ");
            }
            
            await _unitOfWork.PostRepository.Add(post);
        }

        public async Task<bool>UpdatePost(Post post)
        {
            await _unitOfWork.PostRepository.Update(post);
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepository.Delete(id);
            return true;
        }
    }
}
