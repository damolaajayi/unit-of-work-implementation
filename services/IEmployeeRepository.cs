using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWorkTutorial.Models;

namespace UnitOfWorkTutorial.services
{
    public interface IEmployeeRepository : IGenericRepository<CompanyEmployee>
    {
    }
}
