using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContract;
using ServiceContract;
using ServiceContract.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CountriesService : ICountryService
    {
       // private readonly  PersonsDbContext _db;
        private readonly ICountryRepository _countryRepo;
        public CountriesService(PersonsDbContext personsDbContext,ICountryRepository countryRepository)
        {
         //  _db = personsDbContext;
            _countryRepo = countryRepository;
        }
        public async Task<CountryResponseDTO> AddCountry(CountryAddDTO countryAddRequest)
        {
            Country country = countryAddRequest.ToCountry();
            country.CountryId = new Guid();
            await _countryRepo.AddCountry(country);
            return country.ToCountryResponse();
        }

        public async Task<List<CountryResponseDTO>> GetAllCountries()
        {
            var countries= await _countryRepo.GetAllCountries();
            return countries.Select(c => c.ToCountryResponse()).ToList();
        }

        public async Task<CountryResponseDTO?> GetCountryById(Guid? countryId)
        {
            if (countryId == null || countryId == Guid.Empty)
                return null;

            Country? countryResponse = await _countryRepo.GetCountryByCountryID(countryId.Value);//_db.Countries.FirstOrDefaultAsync(country => country.CountryId == countryId);
            if (countryResponse == null)return null;
            return countryResponse.ToCountryResponse();
        }

   
    }
}
