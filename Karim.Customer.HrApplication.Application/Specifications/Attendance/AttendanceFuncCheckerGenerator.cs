using Karim.Customer.HrApplication.Domain.Entities.Attendance;
using Karim.Customer.HrApplication.Shared.DTOs.Attendance;
using System.Linq.Expressions;

namespace Karim.Customer.HrApplication.Application.Specifications.Attendance
{
    internal static class AttendanceFuncCheckerGenerator
    {
        public static Expression<Func<Fingerprint, bool>>? getDateFunc(DateOnly? from, DateOnly? to)
        {
            if (from == null && to == null)
            {
                from = new DateOnly(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
                to = new DateOnly(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
            }
            if (from == null && to != null) from = to;
            if (from != null && to == null) to = from;
            return FB => FB.Date >= from && FB.Date <= to;
        }
        public static Expression<Func<Fingerprint, bool>>? searchByName(string? name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            return FB => FB.Employee.FullNameNormalized.Contains(name.ToUpper());
        }
        public static Expression<Func<Fingerprint, bool>>? getStatusFunc(int? status)
        {
            if (status == null) return null;
            return (FingerprintSatusLockup)status switch
            {
                FingerprintSatusLockup.Delay => FB => FB.Status == FingerprintStatus.Delay,
                FingerprintSatusLockup.Late => FB => FB.Status == FingerprintStatus.Late,
                FingerprintSatusLockup.Active => FB => FB.Status == FingerprintStatus.Active,
                FingerprintSatusLockup.Absense => FB => FB.Status == FingerprintStatus.Absense,
                FingerprintSatusLockup.InActive => FB => FB.Status == FingerprintStatus.InActive,
                _ => null
            };
        }
        public static Expression<Func<Fingerprint, bool>>? getByEmpId(string? empId)
        {
            if (string.IsNullOrEmpty(empId)) return null;
            return FB => FB.EmpId == empId;
        }
        public static Expression<Func<Fingerprint, bool>>? funcCompinor(params Expression<Func<Fingerprint, bool>>[]? expressions)
        {
            if(expressions == null) return null;
            if(expressions.Where(x => x != null).Count() == 0) return null;
            if(expressions.Where(x => x != null).Count() == 1) return expressions.First();
            //Create Parameter
            var parameter = Expression.Parameter(typeof(Fingerprint), "FB");
            //Create List For other part of exprissions
            var conditions = new List<InvocationExpression>();
            //Loop on comming list
            foreach (var item in expressions.Where(e => e is not null).ToList())
            {
                //Get Condition Part
                var condition = Expression.Invoke(item, parameter);
                //Push Into List
                conditions.Add(condition);
            }
            //Create Base Start For Compining 
            var baseCompiner = Expression.AndAlso(conditions[0], conditions[1]);
            //Loop On conditions for the rest of the list
            for (var i = 2; i < conditions.Count(); i++)
            {
                baseCompiner = Expression.AndAlso(baseCompiner, conditions[i]);
            }
            //Compine With Parameter
            var finalExprission = Expression.Lambda<Func<Fingerprint, bool>>(baseCompiner, parameter);
            return finalExprission;
        }
    }
}
