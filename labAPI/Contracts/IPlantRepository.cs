using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPlantRepository
    {
        Task<IEnumerable<Plant>> GetAllPlantsAsync(bool trackChanges);
        Task<Plant> GetPlantAsync(Guid gardenId, Guid id, bool trackChanges);
        void CreatePlantForCompany(Guid gardenId, Plant plant);
        void DeletePlant(Plant plant);
    }
}
