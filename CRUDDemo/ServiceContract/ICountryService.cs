using ServiceContract.DTO;
using System;
using System.Collections.Generic;


namespace ServiceContract
{
    public interface ICountryService
    {
        CountryResponseDTO AddCountry(CountryAddDTO? countryAddRequest);
        List<CountryResponseDTO> GetAllCountries();

        CountryResponseDTO? GetCountryById(Guid? countryId);
    }
}
