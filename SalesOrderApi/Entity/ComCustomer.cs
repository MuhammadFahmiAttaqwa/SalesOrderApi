using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesOrderApi.Entity
{
    [Table("COM_CUSTOMER")]
    public class ComCustomer
    {
        [Key]
        public int COM_CUSTOMER_ID { get; set; }

        public string CUSTOMER_NAME { get; set; }

    }
}
