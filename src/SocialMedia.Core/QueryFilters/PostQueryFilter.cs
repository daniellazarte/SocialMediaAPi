using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.QueryFilters
{
    public class PostQueryFilter
    {
        public int? UserId { get; set; }
        public string Description { get; set; }
        
        //Pagination
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
