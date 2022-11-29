using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class GardenForCreationDto
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "Garden name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Country { get; set; }
        public IEnumerable<PlantForCreationDto> Plants { get; set; }
    }
}
