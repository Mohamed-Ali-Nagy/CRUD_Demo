using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Entities
{
    public class PersonsDbContext:DbContext
    {
        public PersonsDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Person> Persons { get; set;}
        public DbSet<Country> Countries { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Person>().ToTable("Persons");

            string countriesJson = File.ReadAllText("countries.json");
            List<Country>? countries = JsonSerializer.Deserialize<List<Country>>(countriesJson);
            modelBuilder.Entity<Country>().HasData(countries);

            string PersonsJson = File.ReadAllText("persons.json");
            List<Person>? persons = JsonSerializer.Deserialize<List<Person>>(PersonsJson);
            modelBuilder.Entity<Person>().HasData(persons);
        }
    }
}
