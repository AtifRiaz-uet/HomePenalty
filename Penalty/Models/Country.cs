using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;

namespace PenaltyProject.Models
{
    public class Country
    {
        public int countryId;
        public string countryName;
        public string countrycurrency;
        public int penalty;
        public int tax;



    }
}