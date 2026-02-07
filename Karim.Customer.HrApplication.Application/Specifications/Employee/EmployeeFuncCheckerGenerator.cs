using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;
using System.Linq.Expressions;
using Karim.Customer.HrApplication.Domain.Entities.Employee;
using Karim.Customer.HrApplication.Shared.DTOs.Employees;

namespace Karim.Customer.HrApplication.Application.Specifications.Employee
{
    internal static class EmployeeFuncCheckerGenerator
    {
        public static Expression<Func<employee, bool>>? generateWorkTypeFunc(int? workType)
        {
            if (workType == null) return null;
            Expression<Func<employee, bool>>? expression = (EmployeeWorkTypeLockup)workType switch
            {
                EmployeeWorkTypeLockup.FullTime => E => E.WorkType == WorkType.FullTime,
                EmployeeWorkTypeLockup.PartTime => E => E.WorkType == WorkType.PartTime,
                EmployeeWorkTypeLockup.HybridFullTime => E => E.WorkType == WorkType.HybridFullTime,
                EmployeeWorkTypeLockup.HybridPartTime => E => E.WorkType == WorkType.HybridPartTime,
                EmployeeWorkTypeLockup.RemoteFullTime => E => E.WorkType == WorkType.RemoteFullTime,
                EmployeeWorkTypeLockup.RemotePartTime => E => E.WorkType == WorkType.RemotePartTime,
                _ => null
            };
            return expression;
        }
        public static Expression<Func<employee, bool>>? generateEmployeeTypeFunc(int? employeeType)
        {
            if (employeeType == null) return null;
            Expression<Func<employee, bool>>? expression = (EmployeeTypeLockup)employeeType switch
            {
                EmployeeTypeLockup.Freelance => E => E.EmployeeType == EmployeeType.Freelance,
                EmployeeTypeLockup.Contract => E => E.EmployeeType == EmployeeType.Contract,
                EmployeeTypeLockup.LongLife => E => E.EmployeeType == EmployeeType.LongLife,
                _ => null
            };
            return expression;
        }
        public static Expression<Func<employee, bool>>? generateContractFunc(int? contractType)
        {
            if (contractType is null) return null;
            Expression<Func<employee, bool>>? expression = (ContractExistLockup)contractType switch
            {
                ContractExistLockup.HasContract => E => E.IsHasContract == true,
                ContractExistLockup.HasNoContract => E => E.IsHasContract == false,
                _ => null
            };
            return expression;
        }
        public static Expression<Func<employee, bool>>? generateEmployeeStatusFunc(int? employeeStatus)
        {
            if (employeeStatus is null) return null;
            Expression<Func<employee, bool>>? expression = (EmployeeStatusLockup)employeeStatus switch
            {
                EmployeeStatusLockup.Active => E => E.EmployeeStatus == EmployeeStatus.Active,
                EmployeeStatusLockup.InActive => E => E.EmployeeStatus == EmployeeStatus.InActive,
                EmployeeStatusLockup.Terminated => E => E.EmployeeStatus == EmployeeStatus.Terminated,
                EmployeeStatusLockup.Resigned => E => E.EmployeeStatus == EmployeeStatus.Resigned,
                EmployeeStatusLockup.OnLeave => E => E.EmployeeStatus == EmployeeStatus.OnLeave,
                EmployeeStatusLockup.OnVacation => E => E.EmployeeStatus == EmployeeStatus.OnVacation,
                EmployeeStatusLockup.NotTerminated => E => E.EmployeeStatus != EmployeeStatus.Terminated,
                _ => null
            };
            return expression;
        }
        public static Expression<Func<employee, bool>>? generateEmployeeByDepartmentIdFunc(string? departmentId)
        {
            if(departmentId is null) return null;
            return E => E.DepartmentId == departmentId;
        }
        public static Expression<Func<employee, bool>>? generateSearchByNameFunc(string? Name)
        {
            if(Name is null) return null;
            Expression<Func<employee, bool>> expression = E => E.FullNameNormalized.Contains(Name.ToUpper());
            return expression;
        }
        public static Expression<Func<employee, bool>>? generateRankFunc(int? Rank)
        {
            if (Rank is null) return null;
            Expression<Func<employee, bool>>? expression = (EmployeeRankLockup)Rank switch
            {
                EmployeeRankLockup.Intern => E => E.Rank == EmployeeRank.Intern,
                EmployeeRankLockup.Fresh => E => E.Rank == EmployeeRank.Fresh,
                EmployeeRankLockup.Junior => E => E.Rank == EmployeeRank.Junior,
                EmployeeRankLockup.MidLevel => E => E.Rank == EmployeeRank.MidLevel,
                EmployeeRankLockup.Senior => E => E.Rank == EmployeeRank.Senior,
                EmployeeRankLockup.TeamLeader => E => E.Rank == EmployeeRank.TeamLeader,
                EmployeeRankLockup.ProjectManager => E => E.Rank == EmployeeRank.ProjectManager,
                EmployeeRankLockup.Manager => E => E.Rank == EmployeeRank.Manager,
                EmployeeRankLockup.Director => E => E.Rank == EmployeeRank.Director,
                _ => null
            };
            return expression;
        }

        //Compineing Method
        public static Expression<Func<employee, bool>>? FuncCriteriasCompinor(params Expression<Func<employee, bool>>[] expressions)
        {
            //Filter The expressions
            var filteredExpressions = expressions.Where(E => E is not null);
            //Check If Array Is Null
            if (!filteredExpressions.Any()) return null;
            //Check If There Is Atleast 1 Expression
            if(filteredExpressions.Count() == 1) return filteredExpressions.FirstOrDefault();
            //Get Parameter From Employee type
            var parameter = Expression.Parameter(typeof(employee), "E");
            //Extract Condition Part And Save Them Into Array
            List<InvocationExpression> conditions = new List<InvocationExpression>();
            foreach (var exp in expressions)
            {
                //Extract Codition
               var condition = Expression.Invoke(exp, parameter);
                //Push It Into List
                conditions.Add(condition);
            }
            //Create Condition Intiated Variable
            var FinalExpression = Expression.AndAlso(conditions[0], conditions[1]);
            //Loop On conditions For Compine The Rest Of Thhem
            for(int i = 2; i < conditions.Count; i++)
            {
                FinalExpression = Expression.AndAlso(FinalExpression,conditions[i + 1]);
            }
            //Return The Lambada Expressions
            return Expression.Lambda<Func<employee, bool>>(FinalExpression, parameter);
        }
    }
}
