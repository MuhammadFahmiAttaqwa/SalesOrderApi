﻿namespace SalesOrderApi.DTO.SaleOrder.Response
{
    public class SoItemResponse
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public double Total {  get; set; }

    }
}
