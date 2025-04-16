namespace SalesOrderApi.Common
{
    public abstract class PagingRequest
    {
        public int PageSize { get; set; } = 5;

        public int CurrentPage { get; set; } = 1;

    }
}
