using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entities;
using RepositoryContract;
namespace Repository
{
    public class PersonRepository : IPersonRepository
    {
        public Task<Person> AddPerson(Person person)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePersonByPersonID(Guid personID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Person>> GetAllPersons()
        {
            throw new NotImplementedException();
        }

        public Task<List<Person>> GetFilteredPersons(Expression<Func<Person, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Person?> GetPersonByPersonID(Guid personID)
        {
            throw new NotImplementedException();
        }

        public Task<Person> UpdatePerson(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
