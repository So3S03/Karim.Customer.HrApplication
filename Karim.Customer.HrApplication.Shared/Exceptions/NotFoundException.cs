namespace Karim.Customer.HrApplication.Shared.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) :base(message)
        {
            
        }
        public NotFoundException(object Id, string moduleName) : base($"{moduleName} With Id: {Id} Not Found")
        {
            
        }
    }
}
