using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.CustomEntities
{
    public class PagedList<T>: List<T> //Generic Pagination
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;  // SI es mayor a 1 Se puede avanzar
        public bool HasNextPage => CurrentPage < TotalPages; // SI es menor al total de pginas
        public int? NextPageNumber => HasNextPage ? CurrentPage + 1 : (int?)null;
        public int? PreviousPageNumber => HasNextPage ? CurrentPage -1 : (int?)null;

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize); // Para convertir a numero entero 

            AddRange(items);

        }

        public static PagedList<T> Create(IEnumerable<T> source,int pageNumber, int PageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList(); //El valor minimo es 1 
            return new PagedList<T>(items, count, pageNumber, PageSize);
        }
    }
}
