using Hangfire;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Contracts;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Payrolls;

namespace Karim.Customer.HrApplication.Infrastructure.HangfireServices
{
    public static class HangfireServices
    {
        public static void HangfireJobs(IRecurringJobManager recurringJobManager)
        {
            recurringJobManager.AddOrUpdate<IPayrollService>("calc-payrolls",
                (payrollService) => payrollService.CalculateEmployeesPayrolls(),
                 "0 0 28-31 * *",
                 new RecurringJobOptions() { TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time") });
            recurringJobManager.AddOrUpdate<IContractService>("update-expired-contracts",
                contractService => contractService.CheckForExpiredContracts(),
                "0 1 * * *",
                new RecurringJobOptions() {TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time") });
        }
    }
}
