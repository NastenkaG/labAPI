using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPlantRepository
    {
        Task<PagedList<Plant>> GetPlantsAsync(Guid companyId,
            PlantParameters plantParameters, bool trackChanges);
        Task<Plant> GetPlantAsync(Guid gardenId, Guid id, bool trackChanges);
        void CreatePlantForCompany(Guid gardenId, Plant plant);
        void DeletePlant(Plant plant);
    }
}
