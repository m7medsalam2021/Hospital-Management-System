using Hospital.Core.Entities;
using Hospital.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.IRepositories
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        #region Static Way
		
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        #endregion

        #region Dynamic Way

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T> GetByIdWithSpecAsync(ISpecification<T> spec); 
        #endregion

        Task<int> GetCountWithSpecAsync(ISpecification<T> spec);

        Task AddAsync(T entity); // Add Entity to Database

        void Update(T entity);

        void Delete(T entity);
    }
}
