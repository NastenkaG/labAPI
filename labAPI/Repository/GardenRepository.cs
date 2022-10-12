using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Repository
{
    public class GardenRepository : RepositoryBase<Garden>, IGardenRepository
    {
        public GardenRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public IEnumerable<Garden> GetAllGardens(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(g => g.Name).ToList();
    }
}

