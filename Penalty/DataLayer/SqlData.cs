using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using PenaltyProject.Models;
using System.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;

namespace PenaltyProject.DataLayer
{
    public class SqlData : ISqlData
    {
        //public List<Country> countriesList = new List<Country>();

        string conString = "";
        public SqlData(IConfiguration config)
        {
            conString = config.GetConnectionString("Connection");
        }
        
        public List<Country> GetCountries()
        {
            List<Country> countriesList = new List<Country>();
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            string query = "SELECT * FROM COUNTRYDATA";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable countriesTable = new DataTable();
            sda.Fill(countriesTable);


            for (int index = 0; index < countriesTable.Rows.Count; index++)
            {
                Country country = new Country
                {
                    countryId = Convert.ToInt32(countriesTable.Rows[index]["CountryID"]),
                    countryName = countriesTable.Rows[index]["CountryName"].ToString(),
                    countrycurrency = countriesTable.Rows[index]["CountryCurrency"].ToString(),
                    penalty = Convert.ToInt32(countriesTable.Rows[index]["PenaltyPerDay"]),
                    tax = Convert.ToInt32(countriesTable.Rows[index]["TaxPerDay"])
                };
                countriesList.Add(country);
            }

            con.Close();
            return countriesList;
        }

        public int GetDays(datesModel dates)
        {

            List<SpecialDays> specialDates = GetSpecialDays();
            int day = 1;

            while ((dates.checkIn).Date != (dates.checkOut).Date)
            {
                SpecialDays newdays = new SpecialDays();
                newdays.date = dates.checkIn;

                if ((dates.checkIn).DayOfWeek.ToString() == "Saturday" || (dates.checkIn).DayOfWeek.ToString() == "Sunday" || specialDates.Contains(newdays) )
                {
                    // Do Nothing
                }
                else
                {
                    Console.WriteLine($"{(dates.checkIn).Date.ToString("yyyy-MM-dd")}:{(dates.checkIn).DayOfWeek.ToString()}");
                    day++;
                }
                (dates.checkIn) = (dates.checkIn).AddDays(1);
            }
            return day;
        }

        private List<SpecialDays> GetSpecialDays()
        {
            List<SpecialDays> specialDaysList = new List<SpecialDays>();
            const string apiKey = "AIzaSyCkHEq9efc73mgl0k3Ib7wwI54Gle5hX3M";
            const string pakCalendarID = "en-gb.pk#holiday@group.v.calendar.google.com";
            const string uaeCalendarID = "en-gb.ae.official#holiday@group.v.calendar.google.com";


            static async Task Main(string[] args, List<SpecialDays> specialDaysList)
            {
                Console.WriteLine("Just Checking");

                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    ApiKey = apiKey,
                    ApplicationName = "Penalty Calculator"
                });
                var request = service.Events.List(pakCalendarID);
                request.Fields = "items(summary,start,end)";
                var response = await request.ExecuteAsync();
                foreach (var item in response.Items)
                {
                    Console.WriteLine($"Holiday: {item.Summary} start: {item.Start.Date} End: {item.End.Date}");

                    SpecialDays days = new SpecialDays();
                    days.date = DateTime.Parse(item.Start.Date);
                    specialDaysList.Add(days);

                }
                Console.ReadLine();


            }
            return specialDaysList;
        }


    }
}
