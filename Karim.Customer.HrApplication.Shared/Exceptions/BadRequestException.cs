namespace Karim.Customer.HrApplication.Shared.Exceptions
{
    public class BadRequestException(string message = "Invalid Request, Try Again Later") : Exception(message)
    {
    }
}
