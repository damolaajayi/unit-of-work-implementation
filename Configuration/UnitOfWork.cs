using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWorkTutorial.Data;
using UnitOfWorkTutorial.services;

namespace UnitOfWorkTutorial.Configuration
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DBContext _context;
        private readonly ILogger _logger;
        public IEmployeeRepository CompanyEmployee { get; private set; }


        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
