using System;

namespace MVCProject.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string ContactName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string LocalAddress { get; set; }
        public string TelNo { get; set; }
        public string MobileNO { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}