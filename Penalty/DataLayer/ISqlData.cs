using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PenaltyProject.Models;

namespace PenaltyProject.DataLayer
{
    public interface ISqlData
    {
        public List<Country> GetCountries();
        public int GetDays(datesModel dates);
    }
}
