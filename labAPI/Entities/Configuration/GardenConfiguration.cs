using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class GardenConfiguration : IEntityTypeConfiguration<Garden>
    {
        public void Configure(EntityTypeBuilder<Garden> builder)
        {
            builder.HasData
            (
            new Garden
            {
                Id = new Guid("81abbca8-664d-4b20-b5de-024705497d4a"),
                Name = "Tropics",
                Country = "USA"
            },
            new Garden
            {
                Id = new Guid("80abbca8-664d-4b20-b9de-024705497d4a"),
                Name = "Desert",
                Country = "USA"
            }
            );
        }
    }
}
