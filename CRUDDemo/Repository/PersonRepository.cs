using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContract;
namespace Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonsDbContext _db;
        public PersonRepository(PersonsDbContext db)
        {
            _db = db; 
        }
        public async Task<Person> AddPerson(Person person)
        {
            _db.Persons.Add(person);
            await _db.SaveChangesAsync();   
            return person;
        }

        public async Task<bool> DeletePersonByPersonID(Guid personID)
        {
            _db.Persons.RemoveRange(_db.Persons.Where(p => p.PersonID == personID));
            int rowAffected=await _db.SaveChangesAsync();
            return rowAffected > 0;
        }

        public async Task<List<Person>> GetAllPersons()
        {
           return await _db.Persons.Include("Country").ToListAsync();
        }

        public async Task<List<Person>> GetFilteredPersons(Expression<Func<Person, bool>> predicate)
        {
           return await _db.Persons.Include("Country").Where(predicate).ToListAsync();
        }

        public async Task<Person?> GetPersonByPersonID(Guid personID)
        {
            return await _db.Persons.Include("Country").FirstOrDefaultAsync(p=>p.PersonID == personID);
        }

        public async Task<Person> UpdatePerson(Person person)
        {
            Person? matchedPerson =await _db.Persons.FirstOrDefaultAsync(p => person.PersonID == p.PersonID);
            if (matchedPerson == null) return person;

            matchedPerson.Address = person.Address;
            matchedPerson.Gender = person.Gender?.ToString();
            matchedPerson.ReceiveNewsLetters = person.ReceiveNewsLetters;
            matchedPerson.PersonName = person.PersonName;
            matchedPerson.DateOfBirth = person.DateOfBirth;
            matchedPerson.Email = person.Email;
            matchedPerson.CountryID = person.CountryID;
           await _db.SaveChangesAsync();
            return matchedPerson;
        }
    }
}
