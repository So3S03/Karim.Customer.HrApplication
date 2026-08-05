namespace Karim.Customer.HrApplication.Shared.DTOs.Dashboard
{
    public class HiringVsResignedOrTerminatedEmployeesDto
    {
        public required string Month { get; set; }
        public int PersonsHired { get; set; }
        public int PersonsTerminated { get; set; }
    }
}
