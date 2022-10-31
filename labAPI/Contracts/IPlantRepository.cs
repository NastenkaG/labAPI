using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IPlantRepository
    {
        IEnumerable<Plant> GetPlants(Guid gardenId, bool trackChanges);
        Plant GetPlant(Guid gardenId, Guid id, bool trackChanges);
        void CreatePlantForCompany(Guid gardenId, Plant plant);
    }
}
