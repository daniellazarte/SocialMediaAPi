﻿using Microsoft.Extensions.Options;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Exeptions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly PaginationOptions _paginationOptions;
        public PostService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            //_postRepository = postRepository;
            //_userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<Post> GetPost(int id)
        {
            return await _unitOfWork.PostRepository.GetById(id);
        }

        public PagedList<Post> GetPosts(PostQueryFilter filters)
        {
            //Validacion de los filtros
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            //return await _postRepository.GetAll();
            var posts = _unitOfWork.PostRepository.GetAll();
            if(filters.UserId != null)
            {
                posts = posts.Where(x => x.UserId == filters.UserId);
            }

            if (filters.Description != null)
            {
                posts = posts.Where(x => x.Description.ToLower().Contains(filters.Description.ToLower()));
            }

            //Despues de hacer los Filtros se debe proceder a realizar la Paginacion
            var pagePosts = PagedList<Post>.Create(posts, filters.PageNumber, filters.PageSize);

            return pagePosts;
        }

        public async Task InsertarPost(Post post)
        {
            //Regla 1 : Validar si el usuario Existe
            //Regla 2: No esta permitido hacer publicaciones que tengan referencia a Sexo
            //Usando la nueva interfaz para validar
            var user = await _unitOfWork.UserRepository.GetById(post.UserId);
            if(user == null)
            {
                throw new BussinessException("User doesnt Exist, Sorry");
            }
            
            var userPost = await _unitOfWork.PostRepository.GetPostsByUser(post.UserId);
            //if (userPost.Count() < 10)
            //{
            //    var lastPost = userPost.OrderByDescending(x => x.Date).LastOrDefault();
                
            //    TimeSpan totaldias = (TimeSpan)(lastPost.Date - DateTime.Now);
            //    int days = Math.Abs(totaldias.Days);
            //    if (days < 7)
            //    {
            //        throw new Exception("You are not enable to publish ");
            //    }

            //}
            //Regla 2
            if (post.Description.Contains("Sexo"))
            {
                throw new BussinessException("Content Sex not allowed. ");
            }
            
            await _unitOfWork.PostRepository.Add(post);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool>UpdatePost(Post post)
        {
            _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepository.Delete(id);
            return true;
        }
    }
}
