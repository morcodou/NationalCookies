using Microsoft.AspNetCore.Mvc;
using NationalCookies.Data.Interfaces;

namespace NationalCookies.Controllers
{

    public class OrderController : Controller
    {
        private IOrderService _orderService;    


        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;          
        }

        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrders();            

            return View(orders);
        }


        public IActionResult Detail(string id)
        {            
             var order = _orderService.GetOrderById(id);           

            return View(order);
        }

        public IActionResult CancelOrder(string id)
        {
            _orderService.CancelOrder(id);            

            return RedirectToAction("Index");
        }

        public IActionResult PlaceOrder(string id)
        {
            _orderService.PlaceOrder(id);            

            return RedirectToAction("Index");
        }

        public IActionResult AddCookieToOrderLine(string cookieId)
        {
            _orderService.AddCookieToOrder(cookieId);            

            return RedirectToAction("Index", "Order");
        }
    }
}
