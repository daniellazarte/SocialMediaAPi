using System;

namespace SocialMedia.Core.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }

    //public class Publicacion
    //{
    //    public int idPublicacion { get; set; }
    //    public int idUsuario { get; set; }
    //    public DateTime Fecha { get; set; }
    //    public string Descripcion { get; set; }
    //    public string Imagen { get; set; }
    //}
}
