namespace Karim.Customer.HrApplication.Shared.Exceptions
{
    public class NotFoundException(object Id, string moduleName) : Exception($"{moduleName} With Id: {Id} Not Found")
    {
    }
}
