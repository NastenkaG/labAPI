using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IGardenRepository Garden { get; }
        IPlantRepository Plant { get; }
        void Save();
    }
}
