namespace Setur.Services.Contact.Application.Exceptions
{
    public class InvalidInfoTypeException:AppException
    {
        public override string Code { get; } = "invalid_info_type";
        public string InfoType { get; }
        public InvalidInfoTypeException(string infoType)
            : base($"Invalid info type with {infoType}")
        {
            InfoType = infoType;
        }
    }
}
