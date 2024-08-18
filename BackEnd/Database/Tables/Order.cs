using System.ComponentModel.DataAnnotations;

namespace BackEnd.Database.Tables
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Destination { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public string PaymentMethod { get; set; } = "";
    }
}
