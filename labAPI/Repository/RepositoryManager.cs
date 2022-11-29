using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;
        private IGardenRepository _gardenRepository;
        private IPlantRepository _plantRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();

        public ICompanyRepository Company
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_repositoryContext);
                return _companyRepository;
            }
        }
        public IEmployeeRepository Employee
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_repositoryContext);
                return _employeeRepository;
            }
        }
        public IGardenRepository Garden
        {
            get
            {
                if (_gardenRepository == null)
                    _gardenRepository = new GardenRepository(_repositoryContext);
                return _gardenRepository;
            }
        }
        public IPlantRepository Plant
        {
            get
            {
                if (_plantRepository == null)
                    _plantRepository = new PlantRepository(_repositoryContext);
                return _plantRepository;
            }
        }


        IPlantRepository IRepositoryManager.Plant => throw new NotImplementedException();

        public void Save() => _repositoryContext.SaveChanges();

        /*void IRepositoryManager.Save()
        {
            throw new NotImplementedException();
        }*/
    }
}
