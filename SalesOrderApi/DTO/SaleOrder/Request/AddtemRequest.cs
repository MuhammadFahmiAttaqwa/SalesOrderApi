namespace SalesOrderApi.DTO.SaleOrder.Request
{
    public class AddtemRequest
    {
        public string? ItemName { get; set; }

        public int? OrderId { get; set; }

        public int? Quantity { get; set; }

        public double? Price { get; set; }
    }
}
