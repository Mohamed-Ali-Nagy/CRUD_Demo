﻿using ServiceContract.DTO;
using ServiceContract.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract
{
    public interface IPersonService
    { /// <summary>
      /// Addds a new person into the list of persons
      /// </summary>
      /// <param name="personAddRequest">Person to add</param>
      /// <returns>Returns the same person details, along with newly generated PersonID</returns>
      /// <returns>Returns the same person details, along with newly generated PersonID</returns>
        Task<PersonResponseDTO?> AddPerson(PersonAddDTO? personAddDTO);

        /// <summary>
        /// Returns all persons
        /// </summary>
        /// <returns>Returns a list of objects of PersonResponse type</returns>
        Task<List<PersonResponseDTO>> GetAllPersons();

        /// <summary>
        /// Returns the person object based on the given person id
        /// </summary>
        /// <param name="personID">Person id to search</param>
        /// <returns>Returns matching person object</returns>
        Task<PersonResponseDTO?> GetPersonById(Guid? personId);

        /// <summary>
        /// Returns all person objects that matches with the given search field and search string
        /// </summary>
        /// <param name="searchBy">Search field to search</param>
        /// <param name="searchString">Search string to search</param>
        /// <returns>Returns all matching persons based on the given search field and search string</returns>
       Task<List<PersonResponseDTO>> GetFilteredPersons(string searchBy, string? searchString);

        /// <summary>
        /// Returns sorted list of persons
        /// </summary>
        /// <param name="allPersons">Represents list of persons to sort</param>
        /// <param name="sortBy">Name of the property (key), based on which the persons should be sorted</param>
        /// <param name="sortOrder">ASC or DESC</param>
        /// <returns>Returns sorted persons as PersonResponse list</returns>
        public Task<List<PersonResponseDTO>> GetSortedPersons(List<PersonResponseDTO> allPersons, string sortBy, SortOrderOptions sortOrder);

        /// <summary>
        /// Updates the specified person details based on the given person ID
        /// </summary>
        /// <param name="personUpdateRequest">Person details to update, including person id</param>
        /// <returns>Returns the person response object after updation</returns>
       Task<PersonResponseDTO> UpdatePerson(PersonUpdateDTO? personUpdateRequest);
        /// <summary>
        /// Deletes a person based on the given person id
        /// </summary>
        /// <param name="PersonId">PersonID to delete</param>
        /// <returns>Returns true, if the deletion is successful; otherwise false</returns>
       Task<bool> DeletePerson(Guid? personID);
    }
}
