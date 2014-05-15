using System.Data.Entity;
using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using StructureMap;
using TheStore.Web.Data;
using TheStore.Web.Domain;
using TheStore.Web.Infrastructure;
using TheStore.Web.Infrastructure.Alerts;
using TheStore.Web.Infrastructure.Tasks;
using TheStore.Web.Models.Order;

namespace TheStore.Web.Controllers
{
    public class OrderController : TheStoreController
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult View(Guid orderUrl)
        {
            var order = _context.Orders.Include(x => x.OrderItems).SingleOrDefault(x => x.OrderUrl == orderUrl);

            if (order == null)
            {
                return
                    this.RedirectToAction<HomeController>(x => x.Index())
                        .WithError("Заказ с таким идентификатором не найден");
            }

            return View(order);
        }

        public ActionResult QuickOrder(string returnUrl)
        {
            var form = new QuickOrderForm {ReturnUrl = returnUrl};
            return PartialView(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuickOrder(ShoppingCart shoppingCart, QuickOrderForm form)
        {
            var order = new Order();

            order.OrderState = OrderState.QuickOrder;
            order.OrderUrl = Guid.NewGuid();
            order.CreatedAt = DateTime.UtcNow;
            order.OrderNumber = string.Format("{0:yyMMdd}-{1}", order.CreatedAt, order.CreatedAt.Millisecond);

            foreach (var cartItem in shoppingCart.CartItems)
            {
                var orderItem = new OrderItem { ProductId = cartItem.Product.ProductId, Quantity = cartItem.Quantity };
                //_context.OrderItems.Add(orderItem);
                order.OrderItems.Add(orderItem);
            }

            var customer = new Customer();
            customer.FullName = form.Name;
            customer.Phone = form.Phone;
            customer.Email = form.Email;
            customer.Orders.Add(order);

            SubmitOrder(order, customer, new DeliveryDetails());

            return Redirect(form.ReturnUrl);
        }

        public ActionResult Checkout(ShoppingCart shoppingCart, string returnUrl)
        {
            var model = new OrderCheckoutViewModel();
            model.ShoppingCart = shoppingCart;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(ShoppingCart shoppingCart, OrderCheckoutViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model.ReturnUrl);

            var order = new Order();
            order.OrderState = OrderState.New;
            order.OrderUrl = Guid.NewGuid();
            order.CreatedAt = DateTime.UtcNow;
            order.OrderNumber = string.Format("{0:yyMMdd}-{1}", order.CreatedAt, order.CreatedAt.Millisecond);
            order.Description = model.Description;

            foreach (var cartItem in shoppingCart.CartItems)
            {
                var orderItem = new OrderItem {ProductId = cartItem.Product.ProductId, Quantity = cartItem.Quantity};
                //_context.OrderItems.Add(orderItem);
                order.OrderItems.Add(orderItem);
            }

            var customer = new Customer();
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Phone = model.Phone;
            customer.Email = model.Email;
            customer.Orders.Add(order);

            var deliveryDetails = new DeliveryDetails();
            deliveryDetails.City = model.City;
            deliveryDetails.Street = model.Street;
            deliveryDetails.HouseNumber = model.HouseNumber;
            order.DeliveryDetails = deliveryDetails;

            //_context.DeliveryDetails.Add(deliveryDetails);

            //_context.Orders.Add(order);

            //_context.Customers.Add(customer);
            
            //_context.SaveChanges();

            //shoppingCart.Clear();
            
            SubmitOrder(order, customer, deliveryDetails);

            return this.RedirectToAction(x => x.View(order.OrderUrl));
        }

        private void SubmitOrder(Order order, Customer customer, DeliveryDetails deliveryDetails)
        {
            foreach (var orderSubmitter in ObjectFactory.Container.GetAllInstances<IOrderSubmitter>())
            {
                orderSubmitter.SubmitOrder(order, customer, deliveryDetails);
            }
        }
    }
}