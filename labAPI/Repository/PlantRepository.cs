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
    public class PlantRepository : RepositoryBase<Plant>, IPlantRepository
    {
        public PlantRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<Plant>> GetAllPlantsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(e => e.Name).ToListAsync();
        public async Task<Plant> GetPlantAsync(Guid gardenId, Guid id, bool trackChanges) =>
            await FindByCondition(e => e.GardenId.Equals(gardenId) && e.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        public void CreatePlantForCompany(Guid gardenId, Plant plant)
        {
            plant.GardenId = gardenId;
            Create(plant);
        }
        public void DeletePlant(Plant plant)
        {
            Delete(plant);
        }
    }
}
