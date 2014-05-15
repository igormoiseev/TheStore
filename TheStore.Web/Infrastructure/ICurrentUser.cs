using TheStore.Web.Domain;

namespace TheStore.Web.Infrastructure
{
    public interface ICurrentUser
    {
        ApplicationUser User { get; }
    }
}