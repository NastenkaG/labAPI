using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class PlantDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
    }
}
