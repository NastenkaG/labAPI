using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class PlantForCreationDto : PlantForManipulationDto
    {
        /*[Required(ErrorMessage = "Plant name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }
        public string Position { get; set; }*/
    }
}
