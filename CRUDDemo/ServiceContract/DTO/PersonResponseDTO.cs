using Entities;
using ServiceContract.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract.DTO
{
    public class PersonResponseDTO
    {
        public Guid PersonID { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }
        public double? Age { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if(obj is not PersonResponseDTO)
            {
                return false;
            }
            PersonResponseDTO person = (PersonResponseDTO)obj;
            return PersonID == person.PersonID && PersonName == person.PersonName && Email == person.Email && DateOfBirth == person.DateOfBirth && Gender == person.Gender && CountryID == person.CountryID && Address == person.Address && ReceiveNewsLetters == person.ReceiveNewsLetters;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
        public PersonUpdateDTO ToPersonUpdateRequest()
        {
            return new PersonUpdateDTO() { PersonID = PersonID, PersonName = PersonName, Email = Email, DateOfBirth = DateOfBirth, Gender =Gender , Address = Address, CountryID = CountryID, ReceiveNewsLetters = ReceiveNewsLetters };
        }
    }

    public static class PersonExtensions
    {
        /// <summary>
        /// Extension method to convert from Person to PersonResponseDTO
        /// </summary>
        /// <param name="person"> Person Object</param>
        /// <returns> an obj from personresponesDTO</returns>
        public static PersonResponseDTO ToPersonResponseDTO(this Person person)
        {
            return new PersonResponseDTO()
            {
                PersonID = person.PersonID,
                PersonName = person.PersonName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                ReceiveNewsLetters = person.ReceiveNewsLetters,
                Address = person.Address,
                CountryID = person.CountryID,
                Gender = GenderOptions.Male,//Enum.Parse<GenderOptions>(person.Gender),   
                Age = (person.DateOfBirth != null) ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) : null
            };
        }

    }
}
