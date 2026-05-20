namespace Karim.Customer.HrApplication.Shared.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(): base("You Are Not Authhorized To Do This Action")
        {
            
        }
        public ForbiddenException(string message): base(message)
        {
            
        }
    }
}
