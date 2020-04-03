using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationalCookies.Data
{
    public class Order
    {
        public Order(){

            OrderLines = new List<OrderLine>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public double Price { get; set; }

        public string Status { get; set; }

        public List<OrderLine> OrderLines { get; set; }



    }
}
