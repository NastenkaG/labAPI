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
        public Garden GetGarden(Guid gardenId, bool trackChanges) => FindByCondition(g
            => g.Id.Equals(gardenId), trackChanges).SingleOrDefault();
        public void CreateGarden(Garden garden) => Create(garden);
        public IEnumerable<Garden> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
            FindByCondition(x => ids.Contains(x.Id), trackChanges).ToList();
        public void DeleteGarden(Garden garden)
        {
            Delete(garden);
        }
    }
}

