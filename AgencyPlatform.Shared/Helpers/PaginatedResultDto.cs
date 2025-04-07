// En AgencyPlatform.Shared.Helpers
namespace AgencyPlatform.Shared.Helpers
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNext => CurrentPage < TotalPages;
        public bool HasPrevious => CurrentPage > 1;
    }

    public static class PaginationHelper
    {
        public static async Task<PaginatedResult<T>> CreateAsync<T>(
            IQueryable<T> source, int page, int pageSize)
        {
            var count = await Task.FromResult(source.Count());
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedResult<T>
            {
                Items = items,
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = page
            };
        }
    }
}
