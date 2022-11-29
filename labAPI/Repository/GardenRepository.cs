using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Repository
{
    public class GardenRepository : RepositoryBase<Garden>, IGardenRepository
    {
        public GardenRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<Garden>> GetAllGardensAsync(bool trackChanges) => await FindAll(trackChanges)
            .OrderBy(g => g.Name)
            .ToListAsync();
        public async Task<Garden> GetGardenAsync(Guid gardenId, bool trackChanges) =>
            await FindByCondition(g => g.Id.Equals(gardenId), trackChanges)
                .SingleOrDefaultAsync();
        public async Task<IEnumerable<Garden>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToListAsync();
        public void CreateGarden(Garden garden) => Create(garden);
        public void DeleteGarden(Garden garden)
        {
            Delete(garden);
        }
    }
}

