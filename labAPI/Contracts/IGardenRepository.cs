using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IGardenRepository
    {
        IEnumerable<Garden> GetAllGardens(bool trackChanges);
        Garden GetGarden(Guid gardenId, bool trackChanges);
        void CreateGarden(Garden garden);
        IEnumerable<Garden> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteGarden(Garden garden);
    }
}
