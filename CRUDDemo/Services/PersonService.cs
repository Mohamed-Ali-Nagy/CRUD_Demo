using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContract;
using ServiceContract;
using ServiceContract.DTO;
using ServiceContract.Enums;
using ServiceContract.Helpers;

namespace Services
{
    public class PersonService : IPersonService
    {
        //private readonly PersonsDbContext _db;
        private ICountryService _countriesService;
        private readonly IPersonRepository _personRepo;
        public PersonService(/*PersonsDbContext personsDbContext,*/ ICountryService countriesService, IPersonRepository personRepo)
        {
          //  _db = personsDbContext;
            _countriesService = countriesService;
            _personRepo = personRepo;
        }
        private async Task<PersonResponseDTO> ConvertPersonToPersonResponse(Person person)
        {
            PersonResponseDTO personResponse = person.ToPersonResponseDTO();
            // personResponse.Country =await _countriesService.GetCountryById(person.CountryID)?.CountryName;
            var country = await _countriesService.GetCountryById(person.CountryID);
            personResponse.Country = country?.CountryName;
            return personResponse;
        }
        public async Task<PersonResponseDTO?> AddPerson(PersonAddDTO? personAddDTO)
        {
            if (personAddDTO == null) return null;

            ValidationHelper.validationModel(personAddDTO);

            Person person = personAddDTO.ToPerson();
            person.PersonID = new Guid();

            await _personRepo.AddPerson(person);

            return await ConvertPersonToPersonResponse(person);

        }

        public async Task<List<PersonResponseDTO>> GetAllPersons()
        {
            var persons = await _personRepo.GetAllPersons();
            return persons.Select(p => p.ToPersonResponseDTO()).ToList();
        }

        public async Task<PersonResponseDTO?> GetPersonById(Guid? personId)
        {
            if (personId == null) return null;
            var person = await _personRepo.GetPersonByPersonID(personId); //_db.Persons.FirstOrDefaultAsync(x => x.PersonID == personId);
            if (person == null) return null;
            return await ConvertPersonToPersonResponse(person);
        }

        public async Task<List<PersonResponseDTO>> GetFilteredPersons(string searchBy, string? searchString)
        {
            List<Person> matchedPersons = searchBy switch
            {
                nameof(Person.PersonName) => await _personRepo.GetFilteredPersons((p => p.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase))),

                nameof(Person.Email) => await _personRepo.GetFilteredPersons((p => p.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase))),
                nameof(Person.Gender) => await _personRepo.GetFilteredPersons((p => p.Gender.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))),


                nameof(Person.Address) => await _personRepo.GetFilteredPersons((p => p.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase))),

                nameof(Person.DateOfBirth) => await _personRepo.GetFilteredPersons((p => p.DateOfBirth.Value.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))),

                _ => await _personRepo.GetAllPersons(),

            };
            return matchedPersons.Select(p=>p.ToPersonResponseDTO()).ToList();
        }

        public async Task<List<PersonResponseDTO>> GetSortedPersons(List<PersonResponseDTO> allPersons, string sortBy, SortOrderOptions sortOrder)
        {
            if (sortBy == null)
                return allPersons;

            List<PersonResponseDTO> sortedPersons = (sortBy, sortOrder) switch
            {

                (nameof(PersonResponseDTO.PersonName), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.PersonName).ToList(),
                (nameof(PersonResponseDTO.PersonName), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.PersonName).ToList(),
                (nameof(PersonResponseDTO.Email), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.Email).ToList(),
                (nameof(PersonResponseDTO.Email), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.Email).ToList(),
                (nameof(PersonResponseDTO.Address), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.Address).ToList(),
                (nameof(PersonResponseDTO.Address), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.Address).ToList(),
                (nameof(PersonResponseDTO.Age), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.Age).ToList(),
                (nameof(PersonResponseDTO.Age), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.Age).ToList(),
                (nameof(PersonResponseDTO.Country), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.Country).ToList(),
                (nameof(PersonResponseDTO.Country), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.Country).ToList(),
                (nameof(PersonResponseDTO.DateOfBirth), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.DateOfBirth).ToList(),
                (nameof(PersonResponseDTO.DateOfBirth), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.DateOfBirth).ToList(),
                (nameof(PersonResponseDTO.Gender), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.Gender).ToList(),
                (nameof(PersonResponseDTO.Gender), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.Gender).ToList(),
                _ => allPersons,
            };
            return sortedPersons;
        }

        public async Task<PersonResponseDTO> UpdatePerson(PersonUpdateDTO? personUpdateRequest)
        {
            if (personUpdateRequest == null)
                return null!;
            ValidationHelper.validationModel(personUpdateRequest);
   
            Person person =await _personRepo.UpdatePerson(personUpdateRequest.ToPerson());

            return person.ToPersonResponseDTO();

        }

        public async Task<bool> DeletePerson(Guid? personID)
        {
            if (personID == null) return false;
            Person? person = await _personRepo.GetPersonByPersonID(personID);//_db.Persons.FirstOrDefault(p => p.PersonID == personID);
            if (person == null) return false;
            await _personRepo.DeletePersonByPersonID(personID.Value);
            return true;
        }
    }
}
