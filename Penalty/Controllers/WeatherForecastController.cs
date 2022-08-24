using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PenaltyProject.BusinessLayer;
using PenaltyProject.Models;
using PenaltyProject.DataLayer;

namespace PenaltyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PenaltyCalculatorController : ControllerBase
    {
        

        IPenaltyCalculator _penalty;

        public PenaltyCalculatorController(IPenaltyCalculator penaltyCal)
        {
            _penalty = penaltyCal;
        }

        [Route("GetCountriesData")]
        public List<string> GetCountriesData()
        {
            List<Country> countries =_penalty.GetCountries();
            List<string> CountriesName = new List<string>();


            for (int index = 0; index < countries.Count; index++)
            {
                CountriesName.Add(countries[index].countryName);
                //newcountry.countryId = (countries[index].countryId);

            }
            //Console.WriteLine(countries);
             
            return CountriesName;
        }
        [Route("Post")]
        [HttpPost]
        public int Post([FromBody] datesModel dates)
        {
            int days = _penalty.GetDays(dates);
            return days;
        }




    }
}
