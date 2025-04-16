namespace SalesOrderApi.Common
{
    public class PagingResponse<T>
    {
        public int CurrentPage { get; set; }

        public int TotalCount { get; set; } 

        public int RowStart { get; set; }

        public int RowEnd { get; set; }

        public int PageSize { get; set; } 

        public int PageCount { get; set; }

        public List<T> Data { get; set; }

        public PagingResponse(int currenPage, int pageSize, int totalCount, List<T> data)
        {
            CurrentPage = currenPage;
            TotalCount = totalCount;
            PageSize = pageSize;
            PageCount = (int)Math.Ceiling((double)totalCount / PageSize);
            RowStart = ((CurrentPage - 1) * PageSize+1);
            RowEnd = Math.Min(currenPage * pageSize, totalCount);
            Data = data;
        }


    }
}
