using Hospital.Core.Entities;
using System.Linq.Expressions;

namespace Hospital.Core.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T,bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Include { get; set; }

        public Expression<Func<T,object>> OrderBy { get; set; }
        public Expression<Func<T,object>> OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
