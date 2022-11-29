using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IGardenRepository
    {
        Task<IEnumerable<Garden>> GetAllGardensAsync(bool trackChanges);
        Task<Garden> GetGardenAsync(Guid gardenId, bool trackChanges);
        void CreateGarden(Garden garden);
        Task<IEnumerable<Garden>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteGarden(Garden garden);
    }
}
