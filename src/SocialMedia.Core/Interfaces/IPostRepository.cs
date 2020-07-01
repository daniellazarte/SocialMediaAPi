﻿using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetPostsByUser( int userId);
        
        //Task<Post> GetPost(int id);
        //Task InsertarPost(Post post);
        //Task<bool> UpdatePost(Post post);
        //Task<bool> DeletePost(int id);



    }
}
