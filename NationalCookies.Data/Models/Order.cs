using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

        public Order IncludeLines(CookieContext cookieContext)
        {
            OrderLines = cookieContext
                .OrderLines
                .Where(ol => ol.OrderId == Id)
                .Select(ol => ol.IncludCookie(cookieContext, this))
                .ToList();

            return this;
        }
    }
}
