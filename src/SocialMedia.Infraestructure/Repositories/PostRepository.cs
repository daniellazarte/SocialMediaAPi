using System;
using System.Text;
using System.Collections.Generic;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.Threading.Tasks;
using SocialMedia.Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
//using System.Linq;
//using System.Security.Cryptography.X509Certificates;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using SocialMedia.Core.Entities;
//using SocialMedia.Core.Interfaces;
//using SocialMedia.Infrastructure.Data;

namespace SocialMedia.Infraestructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(SocialMediaContext context) : base(context){}
        public async Task<IEnumerable<Post>> GetPostsByUser(int userId)
        {
            return await _entities.Where(x => x.UserId == userId).ToListAsync();
        }
    }

}


//namespace SocialMedia.Infraestructure.Repositories
//{
//    public class PostRepository : IPostRepository
//    {
//        private readonly SocialMediaContext _context;
//        public PostRepository(SocialMediaContext context)
//        {
//            _context = context;
//        }
//        public async Task<IEnumerable<Post>> GetPosts()
//        {
//            var posts = await _context.Post.ToListAsync();
//            //await Task.Delay(10);
//            return posts;
//        }

//        public async Task<Post> GetPost(int id)
//        {
//            var post = await _context.Post.FirstOrDefaultAsync(x => x.PostId == id);
//            return post;
//        }

//        public async Task InsertarPost(Post post)
//        {
//            //var post = await _context.Post.FirstOrDefaultAsync(x => x.PostId == id);
//            //return post;
//            _context.Post.Add(post);
//            await _context.SaveChangesAsync();
//        }

//        //Actualizar un Post
//        public async Task<bool> UpdatePost(Post post)
//        {
//            var currentPost = await GetPost(post.PostId);
//            currentPost.Date = post.Date;
//            currentPost.Description = post.Description;
//            currentPost.Image = post.Image;

//            int rows= await _context.SaveChangesAsync();
//            return rows > 0; // DEVUELVE TRUE SI AL MENOS 1 SE ACTUALIZO

//        }

//        //Eliminar un Post
//        public async Task<bool> DeletePost(int id)
//        {
//            var currentPost = await GetPost(id);
//            _context.Post.Remove(currentPost);

//            int rows = await _context.SaveChangesAsync();
//            return rows > 0; // DEVUELVE TRUE SI AL MENOS 1 SE ACTUALIZO

//        }
//    }
//}
