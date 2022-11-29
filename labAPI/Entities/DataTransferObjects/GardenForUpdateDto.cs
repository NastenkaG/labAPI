using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class GardenForUpdateDto
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public IEnumerable<PlantForCreationDto> Plants { get; set; }
    }
}
