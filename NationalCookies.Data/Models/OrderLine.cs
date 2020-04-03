using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationalCookies.Data
{
    public class OrderLine
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid CookieId { get; set; }
        public Cookie Cookie { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public OrderLine IncludCookie(CookieContext cookieContext)
        {
            Cookie = cookieContext .Cookies.Find(CookieId);
            return this;
        }
        public OrderLine IncludCookie(CookieContext cookieContext, Order order)
        {
            Order = order;
            return IncludCookie(cookieContext);
        }

    }
}
