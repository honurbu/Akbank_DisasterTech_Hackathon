﻿using ATOM.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOM.Repository.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<GatheringCenter> GatheringCenters { get; set; }
        public DbSet<HelpCenter> HelpCenters { get; set; }
        public DbSet<HelpDemand> HelpDemands { get; set; }
        public DbSet<WreckDemand> WreckDemands { get; set; }
        public DbSet<CenterType> CenterTypes { get; set; }
        public DbSet<HelpCenterCategories> HelpCenterCategories { get; set; }
        public DbSet<WreckPopulation> WreckPopulations { get; set; }
        public DbSet<HelpPopulation> HelpPopulations { get; set; }
    }
}
