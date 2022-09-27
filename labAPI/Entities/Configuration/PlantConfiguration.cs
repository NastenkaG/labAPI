﻿using Entities.Models;
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
                Position = "Zemlia",
                GardenId = new Guid("81abbca8-666d-4b20-b5de-024705497d4a")
            },
            new Plant
            {
                Id = new Guid("86dba8c0-d158-41f7-938c-ed49778cb52a"),
                Name = "Flower",
                Position = "Trava",
                GardenId = new Guid("81abbca8-669d-4b20-b5de-024705497d4a")
            },
            new Plant
            {
                Id = new Guid("021ca3c1-0deb-4bbd-ae94-2159a9679811"),
                Name = "Kaktus",
                Position = "Pesok",
                GardenId = new Guid("80abbca8-665d-4b20-b9de-024705497d4a")
            });
        }
    }
}