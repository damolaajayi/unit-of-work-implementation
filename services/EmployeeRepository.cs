using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWorkTutorial.Data;
using UnitOfWorkTutorial.Models;

namespace UnitOfWorkTutorial.services
{
    public class EmployeeRepository : GenericRepository<CompanyEmployee>, IEmployeeRepository
    {
        internal DbSet<CompanyEmployee> _dbSet;
        private readonly ILogger _logger;
        public EmployeeRepository(DBContext context, ILogger logger) : base(context, logger)
        {
            _logger = logger;
            _dbSet = context.Set<CompanyEmployee>();
        }

        public override async Task<IEnumerable<CompanyEmployee>> All()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(EmployeeRepository));
                return new List<CompanyEmployee>();
            }
        }
        public override async Task<bool> Upsert(CompanyEmployee entity)
        {
            try
            {
                var existingUser = await _dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();


                if (existingUser == null)
                    return await Add(entity);

                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.Email = entity.Email;
                existingUser.PhoneNumber = entity.PhoneNumber;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(EmployeeRepository));
                return false;
            }
        }


        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await _dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (exist == null)
                    return false;

                _dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(EmployeeRepository));
                return false;
            }
        }

    }
}
