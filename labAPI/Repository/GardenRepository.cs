using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class GardenRepository : RepositoryBase<GardenRepository>, IGardenRepository
    {
        public GardenRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
    }
}
