using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    }
}
