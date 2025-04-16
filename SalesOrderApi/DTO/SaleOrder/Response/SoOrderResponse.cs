namespace SalesOrderApi.DTO.SaleOrder.Response
{
    public class SoOrderResponse
    {
        public long SoOrderId { get; set; }

        public string OrderNo { get; set; } 

        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }

        public string Address { get; set; }

        public SoItemResponsePaging Items { get; set; }

    }
}
