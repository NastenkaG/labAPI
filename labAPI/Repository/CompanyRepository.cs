using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
    }
}
