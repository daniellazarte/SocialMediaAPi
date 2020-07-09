using SocialMedia.Core.CustomEntities;
                                                    
namespace SocialMedia.Api.Response
{
    public class APIResponse<T> // T quiere decir que es un objeto generifico, y hay que especificarle el tipo de dato con el que va a trabajar
    { 
        public APIResponse(T data)
        {
            Data = data;
        }
        //Esta clase permite manejar las respuestas de nuestra API
        public T Data { get; set;}

        public Metadata Meta  { get; set; }

    }
}
