namespace Karim.Customer.HrApplication.Shared.Exceptions
{
    public class UnauthorizedException: Exception
    {
        public UnauthorizedException() : base("You Are Not Authenticated To See This Application")
        {
            
        }

        public UnauthorizedException(string message) : base(message)
        {
            
        }
    }
}
