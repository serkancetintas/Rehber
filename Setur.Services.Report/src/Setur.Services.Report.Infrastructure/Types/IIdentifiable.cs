namespace Setur.Services.Report.Infrastructure.Types
{
    public interface IIdentifiable<out T>
    {
        T Id { get; }
    }
}
