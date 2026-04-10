using Karim.Customer.HrApplication.Domain.Entities.Projects;
using System.Linq.Expressions;

namespace Karim.Customer.HrApplication.Application.Specifications.Projects
{
    internal static class ProjectCriteriaCompinor
    {
        public static Expression<Func<Project, bool>>? getNameFunc(string? Name)
        {
            if (Name == null) return null;
            return P => P.ProjectName.ToUpper().Contains(Name.ToUpper());
        }

        public static Expression<Func<Project, bool>>? getTypeFunc(int? Type)
        {
            if(Type is null) return null;
            return (ProjectType)Type switch
            {
                ProjectType.Internal => P => P.ProjectType == ProjectType.Internal,
                ProjectType.External => P => P.ProjectType == ProjectType.External,
                ProjectType.RnD => P => P.ProjectType == ProjectType.RnD,
                ProjectType.Consulting => P => P.ProjectType == ProjectType.Consulting,
                ProjectType.Maintanance => P => P.ProjectType == ProjectType.Maintanance,
                _ => null
            };
        }

        public static Expression<Func<Project, bool>>? getStatusFunc(int? Status)
        {
            if (Status is null) return null;
            return (ProjectStatus)Status switch
            {
                ProjectStatus.Draft => P => P.ProjectStatus == ProjectStatus.Draft,
                ProjectStatus.Active => P => P.ProjectStatus == ProjectStatus.Active,
                ProjectStatus.InProgress => P => P.ProjectStatus == ProjectStatus.InProgress,
                ProjectStatus.OnHold => P => P.ProjectStatus == ProjectStatus.OnHold,
                ProjectStatus.Completed => P => P.ProjectStatus == ProjectStatus.Completed,
                ProjectStatus.Cancelled => P => P.ProjectStatus == ProjectStatus.Cancelled,
                _ => null
            };
        }

        public static Expression<Func<Project, bool>>? getDepartmentFunc(string? DepartmentId)
        {
            if (string.IsNullOrWhiteSpace(DepartmentId)) return null;
            return P => P.DepartmentId == DepartmentId;
        }


        public static Expression<Func<Project, bool>>? CritriaCompinor(params Expression<Func<Project, bool>>[] expressions)
        {
            if (expressions is null || expressions.Where(e => e is not null).Count() == 0) return null;
            if(expressions.Where(e => e is not null).Count() == 1) return expressions.Where(e => e is not null).First();
            var parameter = Expression.Parameter(typeof(Project), "P");
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
            var finalExpr = Expression.Lambda<Func<Project, bool>>(baseComp, parameter);
            return finalExpr;
        }
    }
}
