using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Reflection;
using Repository.Extensions.Utility;

namespace Repository.Extensions
{
    public static class RepositoryPlantExtensions
    {
        /*public static IQueryable<Plant> FilterPlant(this IQueryable<Plant> plant, 
            uint minAge, uint maxAge) => plant.Where(e => (e.Age >= minAge && e.Age <= maxAge));*/
        public static IQueryable<Plant> Search(this IQueryable<Plant> plant, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return plant;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return plant.Where(p => p.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Plant> Sort(this IQueryable<Plant> plant, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return plant.OrderBy(p => p.Name);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Plant>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return plant.OrderBy(p => p.Name);

            return plant.OrderBy(orderQuery);
        }
    }
}
