﻿using AM.ApplicationCore.Domain;
using AM.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Infrastructure
{
    public class AMContext:DbContext
    {
        //dbsets
        public DbSet<Flight>Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Traveller> Travellers { get; set; }
        //config cnx type de serveur instance de serveur.....
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;
                                   Initial Catalog=AirportManagement;
                                    Integrated Security=true;
                                    MultipleActiveResultSets=true");

            base.OnConfiguring(optionsBuilder);
        }
        //fluentApi
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.ApplyConfiguration(new PlaneConfiguration()); 
           modelBuilder.Entity<Plane>().HasKey(p=>p.PlaneId);

            modelBuilder.Entity<Plane>().ToTable("MyPlanes");

           modelBuilder.Entity<Plane>().Property(p=>p.Capacity).HasColumnName("PlaneCapacity");
            modelBuilder.ApplyConfiguration(new FlightConfiguration());
           
            //configurer le type detenu (owned type)
            modelBuilder.Entity<Passenger>().OwnsOne(p => p.FullName);
            base.OnModelCreating(modelBuilder);
        }
        //pre convention
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveColumnType("datetime");
            base.ConfigureConventions(configurationBuilder);
        }
    }
}
