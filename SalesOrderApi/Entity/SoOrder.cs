using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesOrderApi.Entity
{
    [Table("SO_ORDER")]
    public class SoOrder
    {
        [Key]
        public long SO_ORDER_ID { get; set; }

        public string ORDER_NO { get; set; }

        public DateTime ORDER_DATE { get; set; }

        public int COM_CUSTOMER_ID { get; set; }

        public string ADDRESS { get; set; }
    }
}
