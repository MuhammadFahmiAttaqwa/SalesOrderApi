using SalesOrderApi.Common;

namespace SalesOrderApi.DTO.SaleOrder.Request
{
    public class GetItemRequest : PagingRequest
    {
        public long OrderId { get; set; }
    }
}
