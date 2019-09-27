using System;
using MiCasaSegura.Models.Core;
using MiCasaSegura.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MiCasaSegura.Services.Database
{
    public class MiCasaSeguraDbContext : IdentityDbContext<User, Role, int>
    {

        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<Device> Devices { get; set; }

        public DbSet<KitType> KitTypes { get; set; }
        public DbSet<Kit> Kits { get; set; }

        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Service> Services { get; set; }

        public MiCasaSeguraDbContext(DbContextOptions<MiCasaSeguraDbContext> options) : base(options)
        {
        }
    }
}