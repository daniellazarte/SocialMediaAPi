using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;

namespace SocialMedia.Infraestructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialMediaContext _context;
        public PostRepository(SocialMediaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _context.Post.ToListAsync();
            //await Task.Delay(10);
            return posts;
        }

        public async Task<Post> GetPost(int id)
        {
            var post = await _context.Post.FirstOrDefaultAsync(x => x.PostId == id);
            return post;
        }

        public async Task InsertarPost(Post post)
        {
            //var post = await _context.Post.FirstOrDefaultAsync(x => x.PostId == id);
            //return post;
            _context.Post.Add(post);
            await _context.SaveChangesAsync();
        }

    }
}
