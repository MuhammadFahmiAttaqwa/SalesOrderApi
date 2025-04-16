namespace SalesOrderApi.DTO.SaleOrder.Response
{
    public class SearchResponse
    {
        public long OrderId { get; set; }
        public string OrderNo { get; set; }
        public string CustomerName { get; set; }

        public string OrderDate { get; set; }
    }
}
