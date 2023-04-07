namespace WebAPI_Version.Services
{
    // Lớp dùng để lấy số trang trong class
    public class PaginatedList<T>:List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }
        public PaginatedList(List<T> items, int count, int pageindex, int pagesize)
        {
            PageIndex = pageindex;
            TotalPage = (int)Math.Ceiling(count / (double)pagesize);
            AddRange(items);
        }
        public static PaginatedList<T> Create(IQueryable<T> source, int pageindex, int pagesize)
        {
            var count = source.Count();
            var items = source.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return new PaginatedList<T>(items, count, pageindex, pagesize);
        }

    }
}
