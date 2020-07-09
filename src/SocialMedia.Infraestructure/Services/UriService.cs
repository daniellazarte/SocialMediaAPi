using SocialMedia.Core.QueryFilters;
using SocialMedia.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Services
{

    public class UriService : IUriService
    {
        private readonly string _baseURI;
        public UriService(string baseURI)
        {
            _baseURI = baseURI;
        }

        public Uri GetPostPaginationURI(PostQueryFilter filter, string actionURL)
        {
            string baseurl = $"{_baseURI}{actionURL}";
            return new Uri(baseurl);
        }
    }
}
