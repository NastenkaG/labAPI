using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    internal class PlantConfiguration : IEntityTypeConfiguration<Plant>
    { 
        public void Configure(EntityTypeBuilder<Plant> builder)
        {
            builder.HasData
            (
            new Plant
            {
                Id = new Guid("88acbca8-684d-4b20-b5de-024705497d4a"),
                Name = "Derevo",
                Position = "Zemlia"
            },
            new Plant
            {
                Id = new Guid("86dba8c0-d158-41f7-938c-ed49778cb52a"),
                Name = "Flower",
                Position = "Trava"
            },
            new Plant
            {
                Id = new Guid("021ca3c1-0deb-4bbd-ae94-2159a9679811"),
                Name = "Kaktus",
                Position = "Pesok"
            });
        }
    }
}
