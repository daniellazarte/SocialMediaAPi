using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;

namespace SocialMedia.Infraestructure.Repositories
{
    public class PostRepository: IPostRepository
    {
        private readonly SocialMediaContext _context;
        public PostRepository(SocialMediaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Post>> GetPost()
        {
            //var posts = Enumerable.Range(1, 10).Select(x => new Post
            //{
            //    PostId = x,
            //    Description = $"Descripcion {x}",
            //    Date = DateTime.Now,
            //    Image = $"https://misapis.com/{x}",
            //    UserId = x * 2
            //});

            var posts = await _context.Post.ToListAsync();
            //await Task.Delay(10);
            return posts;
        }

        
    }
}
