using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IEmployeeRepository Garden { get; }
        IEmployeeRepository Plant { get; }
        void Save();
    }
}
