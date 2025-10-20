using Hospital.Core.Entities;
using Hospital.Core.IRepositories;
using Hospital.Core.Specifications;
using Hospital.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repository.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private readonly HospitalContext _hospitalContext;

        public GenericRepo(HospitalContext hospitalContext)
        {
            _hospitalContext = hospitalContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Patient))
            {
                return (IReadOnlyList<T>)await _hospitalContext.Patients
                    .Include(p => p.Appointments)
                    .ToListAsync();
            }
            else
            {
                return await _hospitalContext.Set<T>().ToListAsync();
                //return await _hospitalContext.Set<T>().
            }
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).ToListAsync();


        public async Task<T> GetByIdAsync(int id)
            => await _hospitalContext.Set<T>().FindAsync(id);

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)

            => await ApplySpecification(spec).FirstOrDefaultAsync();

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).CountAsync();

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
          => SpecificationEvaluator<T>.GetQuery(_hospitalContext.Set<T>(), spec);

        public async Task AddAsync(T entity)
           => await _hospitalContext.Set<T>().AddAsync(entity);


        public void Update(T entity)
           => _hospitalContext.Set<T>().Update(entity);


        public void Delete(T entity)
          => _hospitalContext.Set<T>().Remove(entity);

    }
}
