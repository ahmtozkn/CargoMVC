using Cargo.MVC.Enums;

namespace Cargo.MVC.Models
{
    public class CargoViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }

        public string SenderFirstName { get; set; }

        public string SenderLastName { get; set; }

        public string SenderTelNo { get; set; }

        public string SenderAddress { get; set; }

        public string SenderCity { get; set; }

        public string SenderDistrict { get; set; }
        public string RecieverFirstName { get; set; }

        public string RecieverLastName { get; set; }

        public string RecieverTcNo { get; set; }

        public string RecieverTelNo { get; set; }

        public string RecieverAddress { get; set; }

        public string RecieverCity { get; set; }

        public string RecieverDistrict { get; set; }

        public string ProductName { get; set; }
        public string Contents { get; set; }
        public decimal Weight { get; set; }
        public decimal ShippingPrice { get; set; }
       
        public string CargoNo { get; set; }

        public PaymentEnum PaymentBy { get; set; }
        public CargoStatusEnum Status { get; set; }



    }
}
