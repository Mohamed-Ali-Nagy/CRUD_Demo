using NuGet.Frameworks;
using ServiceContract;
using ServiceContract.DTO;
using Services;
namespace CRUDTest
{
    public class CountriesTest
    {
        private readonly ICountryService _countryService;
        public CountriesTest()
        {
            _countryService = null;//new CountriesService();
        }

        #region AddCountry
        [Fact]
        public void AddCountry_NullCountry()
        {
            //arrange
            CountryAddDTO? countryAddDTO = null;

            Assert.Throws<ArgumentNullException>(() =>
            _countryService.AddCountry(countryAddDTO)

            );


        }
        [Fact]
        public void AddCountry_NullCountryName()
        {
            CountryAddDTO? countryAddDTO = new CountryAddDTO() { CountryName=null};

            Assert.Throws<ArgumentNullException>(() =>

               _countryService.AddCountry(countryAddDTO)
               );
        }
        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            CountryAddDTO? countryAddDTO1 = new CountryAddDTO() { CountryName="Egypt"};
            CountryAddDTO? countryAddDTO2 = new CountryAddDTO() { CountryName="Egypt"};

            Assert.Throws<ArgumentException>(() =>
            {
                _countryService.AddCountry(countryAddDTO1);
                _countryService.AddCountry(countryAddDTO2);
            });
        }

        [Fact]
        public void AddCountry_PropeCountry()
        {
            //arrange 
            CountryAddDTO? countryAddDTO = new CountryAddDTO() { CountryName = "egypt" };

            //act
            var countryResponse= _countryService.AddCountry(countryAddDTO);

            //assert
            Assert.True(countryResponse.CountryId != Guid.Empty);
        }
        #endregion

        #region GetAllCountries
        [Fact]
        public void GetAllCountries_EmptyList()
        {
          List<CountryResponseDTO> countriesList=  _countryService.GetAllCountries();

            Assert.Empty(countriesList);
        }

        #endregion

        #region GetById
        [Fact]
        public void GetCountryById_NullId()
        {
            //arrange
            Guid? countryId = null;
            //act 

           CountryResponseDTO? county= _countryService.GetCountryById(countryId);
            //assert
            Assert.Null(county);

        }
        [Fact]
        public void GetCountryById_ValidId()
        {
            //arrange
            CountryAddDTO countryAddDTO = new CountryAddDTO() { CountryName = "egypt" };
            CountryResponseDTO countryResponse= _countryService.AddCountry(countryAddDTO);

            //act
            CountryResponseDTO? countryResponseDTOFromGet=_countryService.GetCountryById(countryResponse.CountryId);
            // assert
            Assert.Equal(countryResponse, countryResponseDTOFromGet);
        }
        #endregion
    }
}