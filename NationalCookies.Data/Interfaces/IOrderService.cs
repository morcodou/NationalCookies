using System;
using System.Collections.Generic;

namespace NationalCookies.Data.Interfaces
{
    public interface IOrderService
    {
        void AddCookieToOrder(string cookieGuidId);

        List<Order> GetAllOrders();

        Order GetOrderById(string orderGuidId);

        void CancelOrder(string orderGuidId);

        void PlaceOrder(string orderGuidId);

    }
}
