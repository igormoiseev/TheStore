namespace TheStore.Web.Domain
{
    public interface IOrderSubmitter
    {
        void SubmitOrder(Order order, Customer customer, DeliveryDetails deliveryDetails);
    }
}
