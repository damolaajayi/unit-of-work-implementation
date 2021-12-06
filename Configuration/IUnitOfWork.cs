,using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWorkTutorial.services;

namespace UnitOfWorkTutorial.Configuration
{
    public interface IUnitOfWork
    {
        IEmployeeRepository CompanyEmployee { get; }
        Task CompleteAsync();
        void Dispose();
    }
}
