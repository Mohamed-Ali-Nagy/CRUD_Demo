using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract.DTO
{
    public class CountryResponseDTO
    {
        public Guid CountryId { get; set; }
        public string? CountryName { get; set; }

    }
    public static class CountryExtensions
    {
        public static CountryResponseDTO ToCountryResponse(this Country country)
        {
            return new CountryResponseDTO() { CountryId = country.CountryId, CountryName = country.CountryName };
        }
    }
}
