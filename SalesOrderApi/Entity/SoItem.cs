using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesOrderApi.Entity
{
    [Table("SO_ITEM")]
    public class SoItem
    {
        [Key]
        public long SO_ITEM_ID { get; set; }

        public long SO_ORDER_ID { get; set; }

        public string ITEM_NAME { get; set; }

        public int QUANTITY { get; set; }

        public double PRICE { get; set; }
    }
}
