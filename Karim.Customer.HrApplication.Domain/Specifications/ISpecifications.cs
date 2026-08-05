using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using System.Linq.Expressions;

namespace Karim.Customer.HrApplication.Domain.Specifications
{
    //this intetrface will hold all the specs we will need for our entities
    public interface ISpecifications<TEntity, TKey> 
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        //hold the includes
        public ICollection<Expression<Func<TEntity, object>>> IncludeList { get; } //will be get for not allowing anyone from edit on it

        //Crateria for where
        public Expression<Func<TEntity, bool>>? Criteria { get; } //will be used for filteration

        //Order By
        public Expression<Func<TEntity, object>>? OrderBy { get; }
        public Expression<Func<TEntity, object>>? OrderByDesc { get; }

        //pagination
        public int Skip { get; }
        public int Take { get; }
        public bool IsPagination { get; }
    }
}
