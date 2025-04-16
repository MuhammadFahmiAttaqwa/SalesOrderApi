using SalesOrderApi.Common;

namespace SalesOrderApi.DTO.SaleOrder.Request
{
    public class SearchRequest : PagingRequest
    {
        public string? OrderNo {  get; set; }

        public string? OrderDate { get; set; }
    }
}
