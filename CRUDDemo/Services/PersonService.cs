using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities;
using ServiceContract;
using ServiceContract.DTO;
using ServiceContract.Enums;
using ServiceContract.Helpers;

namespace Services
{
    public class PersonService : IPersonService
    {
        private readonly PersonsDbContext _db;
        private  ICountryService _countriesService;
        public PersonService(PersonsDbContext personsDbContext, ICountryService countriesService)
        {
            _db = personsDbContext;
            _countriesService = countriesService;
        }
        private PersonResponseDTO ConvertPersonToPersonResponse(Person person)
        {
            PersonResponseDTO personResponse = person.ToPersonResponseDTO();
            personResponse.Country = _countriesService.GetCountryById(person.CountryID)?.CountryName;
            return personResponse;
        }
        public PersonResponseDTO? AddPerson(PersonAddDTO? personAddDTO)
        {
            if (personAddDTO == null) return null;

            ValidationHelper.validationModel(personAddDTO);

            Person person = personAddDTO.ToPerson();
            person.PersonID = new Guid();

           _db.Persons.Add(person);
            _db.SaveChanges();

         return ConvertPersonToPersonResponse(person);

        }

        public List<PersonResponseDTO> GetAllPersons()
        {
            List<PersonResponseDTO> personResponseDTOs=_db.Persons.Select(p=>p.ToPersonResponseDTO()).ToList();
            return personResponseDTOs;
        }

        public PersonResponseDTO? GetPersonById(Guid? personId)
        {
            if(personId == null) return null;
            var person = _db.Persons.FirstOrDefault(x => x.PersonID == personId);
            if(person == null) return null;
            return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponseDTO> GetFilteredPersons(string searchBy, string? searchString)
        {
            List<PersonResponseDTO> matchedPersons = GetAllPersons();
            if (string.IsNullOrEmpty(searchString)||string.IsNullOrEmpty(searchBy))
            {
                return matchedPersons;
            }
            switch (searchBy)
            {
                case nameof(Person.PersonName):
                    matchedPersons=matchedPersons.Where(p=>(!string.IsNullOrEmpty(p.PersonName)?p.PersonName.Contains(searchString,StringComparison.OrdinalIgnoreCase):true)).ToList();
                    break;

                case nameof(Person.Email):
                    matchedPersons = matchedPersons.Where(p => (!string.IsNullOrEmpty(p.Email) ? p.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.Gender):
                    matchedPersons = matchedPersons.Where(p => (!string.IsNullOrEmpty(p.Gender.ToString()) ? p.Gender.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.Address):
                    matchedPersons = matchedPersons.Where(p => (!string.IsNullOrEmpty(p.Address) ? p.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.DateOfBirth):
                    matchedPersons = matchedPersons.Where(p => (p.DateOfBirth!=null ? p.DateOfBirth.Value.ToString("dd mm yyyy").Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                default:matchedPersons = matchedPersons;
                    break;
            }
            return matchedPersons;
        }

        public List<PersonResponseDTO> GetSortedPersons(List<PersonResponseDTO> allPersons, string sortBy, SortOrderOptions sortOrder)
        {
               if(sortBy==null)
                return allPersons;

           List<PersonResponseDTO> sortedPersons = (sortBy, sortOrder) switch
            {
                (nameof(PersonResponseDTO.PersonName),SortOrderOptions.ASC)=>allPersons.OrderBy(p=>p.PersonName).ToList(),
                (nameof(PersonResponseDTO.PersonName),SortOrderOptions.DESC)=>allPersons.OrderByDescending(p=>p.PersonName).ToList(),
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
                 _=>allPersons,
            };
            return sortedPersons;
        }

        public PersonResponseDTO UpdatePerson(PersonUpdateDTO? personUpdateRequest)
        {
            if (personUpdateRequest == null)
                return null!;
            ValidationHelper.validationModel(personUpdateRequest);
            Person? person =_db.Persons.FirstOrDefault(p=>p.PersonID == personUpdateRequest.PersonID);
            if (person == null) return null!;

            person.Address = personUpdateRequest.Address;
            person.Gender=personUpdateRequest.Gender.ToString();
            person.ReceiveNewsLetters= personUpdateRequest.ReceiveNewsLetters;
            person.PersonName = personUpdateRequest.PersonName;
            person.DateOfBirth = personUpdateRequest.DateOfBirth;
            person.Email = personUpdateRequest.Email;
            person.CountryID = personUpdateRequest.CountryID;
            _db.SaveChanges();
            return person.ToPersonResponseDTO();
           
        }

        public bool DeletePerson(Guid? personID)
        {
            if (personID == null) return false;
            Person? person = _db.Persons.FirstOrDefault(p=>p.PersonID==personID);
            if(person == null) return false;
            _db.Persons.Remove(person);
            _db.SaveChanges();
            return true;
        }
    }
}
