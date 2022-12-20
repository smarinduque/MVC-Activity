using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public double TotalAmount { get; set; }
        public CustomerDTO customer { get; set; }
        public List<Order_itemsDTO> Items { get; set; }
    }
}