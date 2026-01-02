namespace Karim.Customer.HrApplication.Shared.Exceptions
{
    public class NotFoundException(string Id, string moduleName) : Exception($"{moduleName} With Id: {Id} Not Found")
    {
    }
}
