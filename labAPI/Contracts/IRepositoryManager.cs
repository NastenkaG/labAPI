using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IGardenRepository Garden { get; }
        IPlantRepository Plant { get; }
        Task SaveAsync();
        //void Save();
    }
}
