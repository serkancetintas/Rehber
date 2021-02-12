namespace Setur.Services.Contact.Infrastructure.Types
{
    public interface IIdentifiable<out T>
    {
        T Id { get; }
    }
}
