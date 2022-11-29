using System;
using System.Collections.Generic;
using System.Text;
using Entities.RequestFeatures;

namespace Entities.RequestFeatures
{
    public class PlantParameters : RequestParameters
    {
        public PlantParameters()
        {
            OrderBy = "name";
        }
        public string SearchTerm { get; set; }
    }
}
