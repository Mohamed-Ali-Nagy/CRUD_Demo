using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContract;
using ServiceContract.DTO;
using ServiceContract.Enums;
using Services;

namespace CRUDDemo.Controllers
{
    [Route("[controller]")]
    public class PersonsController : Controller
    {
        private IPersonService _personService;
        private readonly ICountryService _countryService;
        public PersonsController(IPersonService personService, ICountryService countryService)
        {
            _personService = personService;
            _countryService = countryService;

        }

        [Route("[action]")]
        [Route("/")]
        public IActionResult Index(string searchBy, string? searchString, string sortBy = nameof(PersonResponseDTO.PersonName), SortOrderOptions sortOrder = SortOrderOptions.ASC)
        {
            ViewData["SearchFields"] = new Dictionary<string, string>()
            {
                 
                  { nameof(PersonResponseDTO.PersonName), "Person Name" },
                  { nameof(PersonResponseDTO.Email), "Email" },
                  { nameof(PersonResponseDTO.DateOfBirth), "Date of Birth" },
                  { nameof(PersonResponseDTO.Gender), "Gender" },
                  { nameof(PersonResponseDTO.CountryID), "Country" },
                  { nameof(PersonResponseDTO.Address), "Address" }
            };
            var filterdPersons = _personService.GetFilteredPersons(searchBy,searchString);
            ViewData["CurrentSearchBy"] = searchBy;    
            ViewData["CurrentSearchString"] = searchString;
            var sortedPersons = _personService.GetSortedPersons(filterdPersons, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();
            return View(sortedPersons);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Create()
        {
            var countries=_countryService.GetAllCountries();
            ViewBag.Countries = countries.Select(c=>new SelectListItem() { Text=c.CountryName,Value=c.CountryId.ToString()});
            return View();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Create(PersonAddDTO personAddDTO)
        {
            if (!ModelState.IsValid)
            {
                var countries = _countryService.GetAllCountries();
                ViewBag.Countries = countries.Select(c => new SelectListItem() { Text = c.CountryName, Value = c.CountryId.ToString() });
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View(personAddDTO);
            }

            PersonResponseDTO? persons =_personService.AddPerson(personAddDTO);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("[action]/{personId}")]
        public IActionResult Edit(Guid personId)
        {
            PersonResponseDTO? personResponseDTO=_personService.GetPersonById(personId);
            if (personResponseDTO == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var countries = _countryService.GetAllCountries();
            ViewBag.Countries = countries.Select(c => new SelectListItem() { Text = c.CountryName, Value = c.CountryId.ToString() });
            return View(personResponseDTO.ToPersonUpdateRequest());
        }
        [HttpPost]
        [Route("[action]/{personId}")]
        public IActionResult Edit(PersonUpdateDTO personUpdateDTO)
        {
            if(!ModelState.IsValid)
            {
                var countries = _countryService.GetAllCountries();
                ViewBag.Countries = countries.Select(c => new SelectListItem() { Text = c.CountryName, Value = c.CountryId.ToString() });
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
               
                return View(personUpdateDTO);
            }
            PersonResponseDTO? personResponseDTO = _personService.GetPersonById(personUpdateDTO.PersonID);
            if (personResponseDTO == null)
            {
                return RedirectToAction(nameof(Index));
            }
            _personService.UpdatePerson(personUpdateDTO);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        [Route("[action]/{personId}")]
        public IActionResult Delete(Guid personId)
        {
            PersonResponseDTO? person=_personService.GetPersonById(personId);
            if (person == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        [HttpPost]
        [Route("[action]/{personId}")]
        public IActionResult Delete(PersonUpdateDTO personUpdateDTO )
        {
            PersonResponseDTO? person = _personService.GetPersonById(personUpdateDTO.PersonID);
            if (person == null)
            {
                return RedirectToAction(nameof(Index));
            }
            _personService.DeletePerson(personUpdateDTO.PersonID);
            return RedirectToAction(nameof(Index));
        }
    } 
}
