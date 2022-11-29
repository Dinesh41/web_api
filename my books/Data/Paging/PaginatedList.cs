namespace my_books.Data.Paging
{
    public class PaginatedList<T>:List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public bool HasPreviousPage
        {
            get
            {
                return PageIndex > 1;
            }
        }
        public bool HasNextPage
        {
            get { return PageIndex < TotalPages; }
        }

        public PaginatedList(List<T> items, int pageIndex, int TotalPages)
        {
            this.PageIndex=pageIndex;
            this.TotalPages = TotalPages;
            this.AddRange(items);
        }

        public static PaginatedList<T> create(IQueryable<T> source,int pageIndex,int PageSize)
        {
            var count = source.Count();
            var items=source.Skip((pageIndex-1)*PageSize).Take(PageSize).ToList();
            var totalPages = (int)Math.Ceiling(count / (double) PageSize);
            return new PaginatedList<T>(items, pageIndex, totalPages);
        }
    }
}
