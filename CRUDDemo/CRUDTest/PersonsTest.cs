using Entities;
using ServiceContract;
using ServiceContract.DTO;
using ServiceContract.Enums;
using Services;

namespace CRUDTest
{
    public class PersonsTest
    {
        private readonly IPersonService _personService;

        public PersonsTest()
        {
            _personService = null;//new PersonService();
        }

        #region Add person
        [Fact]
        public void AddPerson_NullPerson()
        {
            //arrange 
            PersonAddDTO? person = null;
            //assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                _personService.AddPerson(person);
            });
        }

        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            //Arrange
            PersonAddDTO? personAddDTO = new PersonAddDTO() { PersonName = "Person name...", Email = "person@example.com", Address = "sample address", CountryID = Guid.NewGuid(), Gender = GenderOptions.Male, DateOfBirth = DateTime.Parse("2000-01-01"), ReceiveNewsLetters = true };

            //Act
            PersonResponseDTO? person_response_from_add = _personService.AddPerson(personAddDTO);

            List<PersonResponseDTO> persons_list = _personService.GetAllPersons();

            //Assert
            Assert.True(person_response_from_add?.PersonID != Guid.Empty);

            Assert.Contains(person_response_from_add, persons_list);
        }
        #endregion

        #region Get Person By Id
        [Fact]
        public void GetPersonById_NullID()
        {
            PersonResponseDTO? personResponseDTO= _personService.GetPersonById(null);

            Assert.Null(personResponseDTO);
           
        }
        #endregion

        
    }
}
