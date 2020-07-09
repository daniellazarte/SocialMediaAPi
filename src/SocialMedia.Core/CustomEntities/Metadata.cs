namespace SocialMedia.Core.CustomEntities
{
    public class Metadata
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int Totalpages { get; set; }
        public bool HasNexttPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public string NextPageUrl { get; set; }
        public string PreviousPageUrl { get; set; }
    }
}
