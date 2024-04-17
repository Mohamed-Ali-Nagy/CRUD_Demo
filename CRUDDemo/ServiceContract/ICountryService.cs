using ServiceContract.DTO;
using System;
using System.Collections.Generic;


namespace ServiceContract
{
    public interface ICountryService
    {
        Task<CountryResponseDTO> AddCountry(CountryAddDTO countryAddRequest);
       Task<List<CountryResponseDTO>> GetAllCountries();

        Task<CountryResponseDTO?> GetCountryById(Guid? countryId);
    }
}
