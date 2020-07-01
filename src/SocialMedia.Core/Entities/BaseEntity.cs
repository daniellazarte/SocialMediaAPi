namespace SocialMedia.Core.Entities
{
    public abstract class BaseEntity //Abstracta por que no vamos a generar instancias de clase, solo para heredar.
    {
        public int id { get; set; }
    }
}
