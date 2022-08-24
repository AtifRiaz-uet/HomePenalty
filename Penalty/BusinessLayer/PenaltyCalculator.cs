using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PenaltyProject.DataLayer;
using PenaltyProject.Models;
namespace PenaltyProject.BusinessLayer
{
    public class PenaltyCalculator :IPenaltyCalculator
    {
        ISqlData _content;

        public PenaltyCalculator(ISqlData content)
        {
            _content = content;
        }

        public List<Country> GetCountries()
        {
            return _content.GetCountries();
        }
        public int GetDays(datesModel dates)
        {
            return _content.GetDays(dates);
        }
    }
}
