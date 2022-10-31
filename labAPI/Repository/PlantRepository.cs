using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Repository
{
    public class PlantRepository : RepositoryBase<Plant>, IPlantRepository
    {
        public PlantRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public IEnumerable<Plant> GetPlants(Guid gardenId, bool trackChanges) =>
            FindByCondition(p => p.GardenId.Equals(gardenId), trackChanges)
                .OrderBy(p => p.Name);
        public Plant GetPlant(Guid gardenId, Guid id, bool trackChanges) =>
            FindByCondition(p => p.GardenId.Equals(gardenId) && p.Id.Equals(id), trackChanges).SingleOrDefault();
        public void CreatePlantForCompany(Guid gardenId, Plant plant)
        {
            plant.GardenId = gardenId;
            Create(plant);
        }
    }
}
