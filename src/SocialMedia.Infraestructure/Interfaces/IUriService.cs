using SocialMedia.Core.QueryFilters;
using System;

namespace SocialMedia.Infraestructure.Interfaces
{
    public interface IUriService
    {
        Uri GetPostPaginationURI(PostQueryFilter filter, string actionURL);
    }
}