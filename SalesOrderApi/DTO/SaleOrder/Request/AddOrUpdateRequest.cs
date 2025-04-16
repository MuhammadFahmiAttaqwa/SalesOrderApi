namespace SalesOrderApi.DTO.SaleOrder.Request
{
    public class AddOrUpdateRequest
    {
        public long? OrderId { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }

        public string Address { get; set; }

        public List<AddtemRequest>? Items { get; set; }

    }
}
