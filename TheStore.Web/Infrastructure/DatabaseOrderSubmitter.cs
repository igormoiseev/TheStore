using TheStore.Web.Data;
using TheStore.Web.Domain;

namespace TheStore.Web.Infrastructure
{
    public class DatabaseOrderSubmitter : IOrderSubmitter
    {
        private readonly ApplicationDbContext _dbContext;

        public DatabaseOrderSubmitter(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void SubmitOrder(Order order, Customer customer, DeliveryDetails deliveryDetails)
        {
            order.DeliveryDetails = deliveryDetails;
            customer.Orders.Add(order);

            _dbContext.DeliveryDetails.Add(deliveryDetails);
            _dbContext.Orders.Add(order);
            _dbContext.Customers.Add(customer);

            _dbContext.SaveChanges();
        }
    }
}