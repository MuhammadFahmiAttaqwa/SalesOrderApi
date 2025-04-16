namespace SalesOrderApi.Common
{
    public class ApiResult<T>
    {
        public string Msg { get; set; }

        public int Code { get; set; }

        public bool Success { get; set; }

        public List<T> Data { get; set; }


    }
}
