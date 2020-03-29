using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SiempreSeCaeElSistema.Models
{
    public class AirlineContext:DbContext
    {
        public AirlineContext(DbContextOptions<AirlineContext> options) : base(options){ }

        public DbSet<Aircraft> Aircrafts { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<FlightAssignedTo> FlightAssignedEmps { get; set; }
    }
}
