using Karim.Customer.HrApplication.Domain.Entities.Projects;
using Karim.Customer.HrApplication.Domain.Entities.Tasks;
using System.Linq.Expressions;
using status = Karim.Customer.HrApplication.Domain.Entities.Tasks.TaskStatus;
using task = Karim.Customer.HrApplication.Domain.Entities.Tasks.Tasks;

namespace Karim.Customer.HrApplication.Application.Specifications.Tasks
{
    internal static class TaskCriteriaCompinor
    {
        public static Expression<Func<task, bool>>? getName(string? Name)
        {
            if (string.IsNullOrEmpty(Name))
                return null;
            return x => x.Name.ToLower().Contains(Name.ToLower());
        }

        public static Expression<Func<task, bool>>? getType(int? Type)
        {
            if (Type is null) return null;
            return (TaskType)Type switch
            {
                TaskType.Project => x => x.Type == TaskType.Project,
                TaskType.Ticket => x => x.Type == TaskType.Ticket,
                _ => null
            };
        }

        public static Expression<Func<task, bool>>? getStatus(int? Status)
        {
            if (Status is null) return null;
            return (status)Status switch
            {
                status.New => x => x.Status == status.New,
                status.InProgress => x => x.Status == status.InProgress,
                status.ReOpened => x => x.Status == status.ReOpened,
                status.Closed => x => x.Status == status.Closed,
                _ => null
            };
        }

        public static Expression<Func<task, bool>>? getProject(string? ProjectId)
        {
            if (string.IsNullOrEmpty(ProjectId)) return null;
            return x => x.ProjectId == ProjectId;
        }

        public static Expression<Func<task, bool>>? getTicket(string? TicketId)
        {
            if (string.IsNullOrEmpty(TicketId)) return null;
            return x => x.TicketId == TicketId;
        }

        public static Expression<Func<task, bool>>? getEmployee(string? EmployeeId)
        {
            if (string.IsNullOrEmpty(EmployeeId)) return null;
            return x => x.EmployeeId == EmployeeId;
        }

        public static Expression<Func<task, bool>>? getArchived(bool? isArchived)
        {
            if (isArchived is null) return null;
            return x => x.isArchived == isArchived;
        }

        public static Expression<Func<task, bool>>? CritriaCompinor(params Expression<Func<task, bool>>[] expressions)
        {
            if (expressions is null || expressions.Where(e => e is not null).Count() == 0) return null;
            if (expressions.Where(e => e is not null).Count() == 1) return expressions.Where(e => e is not null).First();
            var parameter = Expression.Parameter(typeof(task), "T");
            var ivocationList = new List<InvocationExpression>();
            foreach (var exp in expressions.Where(e => e is not null))
            {
                var e = Expression.Invoke(exp, parameter);
                ivocationList.Add(e);
            }
            var baseComp = Expression.AndAlso(ivocationList[0], ivocationList[1]);
            for (int i = 2; i < ivocationList.Count; i++)
            {
                baseComp = Expression.AndAlso(baseComp, ivocationList[i]);
            }
            var finalExpr = Expression.Lambda<Func<task, bool>>(baseComp, parameter);
            return finalExpr;
        }
    }
}
