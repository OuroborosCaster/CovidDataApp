using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace CovidDataApp
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Cases
    {
        public string @new { get; set; } = "None";
        public string active { get; set; } = "None";
        public string critical { get; set; } = "None";
        public string recovered { get; set; } = "None";
        public string M_pop { get; set; }
        public string total { get; set; } = "None";
    }

    public class Deaths
    {
        public string @new { get; set; } = "None";
        public string M_pop { get; set; }
        public string total { get; set; } = "None";
    }

    public class Tests
    {
        public string M_pop { get; set; }
        public string total { get; set; } = "None";
    }

    public class Response
    {
        public string continent { get; set; } = "None";
        public string country { get; set; } = "None";
        public string population { get; set; } = "None";
        public Cases? cases { get; set; }
        public Deaths? deaths { get; set; }
        public Tests? tests { get; set; }
        public string day { get; set; } = "None";
        public DateTime? time { get; set; }
    }

    public class Root
    {
        public string get { get; set; } = "None";
        public List<object>? parameters { get; set; }
        public List<object>? errors { get; set; }
        public int results { get; set; } = 0;
        public List<Response>? response { get; set; }
    }

    class ParseData
    {
        //Response
        private string[] ListContinent = { "None" };
        private string[] ListCountry = { "None" };
        private string[] ListPopulation = { "None" };
        private string[] ListDay = { "None" };
        private string[] ListTime = { "None" };
        //Cases
        private string[] ListNewCases = { "None" };
        private string[] ListActiveCases = { "None" };
        private string[] ListCriticalCases = { "None" };
        private string[] ListRecoveredCases = { "None" };
        private string[] CasesM_pop;
        private string[] ListTotalCases = { "None" };
        //Deaths
        private string[] ListNewDeaths = { "None" };
        private string[] DeathsM_pop;
        private string[] ListTotalDeaths = { "None" };
        //Tests
        private string[] TestsM_pop ;
        private string[] ListTotalTests = { "None" };


        public void ParseJSON()
        {
            DataAPI data = new DataAPI();
            data.AccessAsync().Wait();
            string json = data.Get();
            json=json.Replace("1M", "M");
            JavaScriptSerializer js = new JavaScriptSerializer();
            Root root = js.Deserialize<Root>(json);
            int results = root.results;
            //Response
            setListContinent(root, results);
            setListCountry(root, results);
            setListPopulation(root, results);
            setListDay(root, results);
            setListTime(root, results);
            //Cases
            setListNewCases(root, results);
            setListActiveCases(root, results);
            setListCriticalCases(root, results);
            setListRecoveredCases(root, results);
            setCasesM_pop(root, results);
            setListTotalCases(root, results);
            //Deaths
            setListNewDeaths(root, results);
            setDeathsM_pop(root, results);
            setListTotalDeaths(root, results);
            //Tests
            setTestsM_pop(root, results);
            setListTotalTests(root, results);
        }
        //Setter
        private void setListTotalTests(Root root, int results)
        {
            ListTotalTests = new string[results];
            for (int i = 0; i < results; i++)
            {
                if (root.response[i].tests.total == null)
                {
                    ListTotalTests[i] = "None";
                }
                else
                {
                    ListTotalTests[i] = root.response[i].tests.total;
                }
            }
        }

        private void setTestsM_pop(Root root, int results)
        {
            TestsM_pop = new string[results];
            for (int i = 0; i < results; i++)
            {
                if (root.response[i].tests.M_pop == null)
                {
                    TestsM_pop[i] = "None";
                }
                else
                {
                    TestsM_pop[i] = root.response[i].tests.M_pop;
                }
            }
        }

        private void setListTotalDeaths(Root root, int results)
        {
            ListTotalDeaths = new string[results];
            for (int i = 0; i < results; i++)
            {
                if (root.response[i].deaths.total == null)
                {
                    ListTotalDeaths[i] = "None";
                }
                else
                {
                    ListTotalDeaths[i] = root.response[i].deaths.total;
                }
            }
        }

        private void setDeathsM_pop(Root root, int results)
        {
            DeathsM_pop = new string[results];
            for (int i = 0; i < results; i++)
            {
                if (root.response[i].deaths.M_pop == null)
                {
                    DeathsM_pop[i] = "None";
                }
                else
                {
                    DeathsM_pop[i] = root.response[i].deaths.M_pop;
                }
            }
        }

        private void setListNewDeaths(Root root, int results)
        {
            ListNewDeaths = new string[results];
            for (int i = 0; i < results; i++)
            {
                if (root.response[i].deaths.@new == null)
                {
                    ListNewDeaths[i] = "None";
                }
                else
                {
                    ListNewDeaths[i] = root.response[i].deaths.@new;
                }
            }
        }

        private void setListTotalCases(Root root, int results)
        {
            ListTotalCases = new string[results];
            for (int i = 0; i < results; i++)
            {
                if (root.response[i].cases.total == null)
                {
                    ListTotalCases[i] = "None";
                }
                else
                {
                    ListTotalCases[i] = root.response[i].cases.total;
                }
            }
        }

        private void setCasesM_pop(Root root, int results)
        {
            CasesM_pop = new string[results];
            for (int i = 0; i < results; i++)
            {
                if (root.response[i].cases.M_pop == null)
                {
                    CasesM_pop[i] = "None";
                }
                else
                {
                    CasesM_pop[i] = root.response[i].cases.M_pop;
                }
            }
        }

        private void setListRecoveredCases(Root root, int results)
        {
            ListRecoveredCases = new string[results];
            for (int i = 0; i < results; i++)
            {
                if (root.response[i].cases.recovered == null)
                {
                    ListRecoveredCases[i] = "None";
                }
                else
                {
                    ListRecoveredCases[i] = root.response[i].cases.recovered;
                }
            }
        }

        private void setListCriticalCases(Root root, int results)
        {
            ListCriticalCases = new string[results];
            for (int i = 0; i < results; i++)
            {
                if (root.response[i].cases.critical == null)
                {
                    ListCriticalCases [i] = "None";
                }
                else
                {
                    ListCriticalCases[i] = root.response[i].cases.critical;
                }
            }
        }

        private void setListActiveCases(Root root, int results)
        {
            ListActiveCases = new string[results];
            for (int i = 0; i < results; i++)
            {
                if (root.response[i].cases.active == null)
                {
                    ListActiveCases [i] = "None";
                }
                else
                {
                    ListActiveCases[i] = root.response[i].cases.active;
                }
            }
        }
        private void setListNewCases(Root root, int results)
        {
            ListNewCases = new string[results];
            for (int i = 0; i < results; i++)
            {
                if (root.response[i].cases.@new == null)
                {
                    ListNewCases[i] = "None";
                }
                else
                {
                    ListNewCases[i] = root.response[i].cases.@new;
                }
            }
        }
        private void setListTime(Root root, int results)
        {
            ListTime = new string[results];
            for (int i = 0; i < results; i++)
            {
                if (root.response[i].time == null)
                {
                    ListTime[i] = "None";
                }
                else
                {
                    ListTime[i] = root.response[i].time.ToString();
                }
            }
        }

        private void setListDay(Root root, int results)
        {
            ListDay = new string[results];
            for (int i = 0; i < results; i++)
            {
                if (root.response[i].day == null)
                {
                    ListDay[i] = "None";
                }
                else
                {
                    ListDay[i] = root.response[i].day;
                }
            }
        }

        private void setListPopulation(Root root, int results)
        {
            ListPopulation = new string[results];
            for (int i = 0; i < results; i++)
            {
                if (root.response[i].population == null)
                {
                    ListPopulation[i] = "None";
                }
                else
                {
                    ListPopulation[i] = root.response[i].population;
                }
            }
        }

        private void setListCountry(Root root, int results)
        {
            ListCountry = new string[results];
            for (int i = 0; i < results; i++)
            {
                if (root.response[i].country == null)
                {
                    ListCountry[i] = "None";
                }
                else
                {
                    ListCountry[i] = root.response[i].country;
                }
            }

        }

        private void setListContinent(Root root, int results)
        {
            ListContinent = new string[results];
            for (int i = 0; i < results; i++)
            {
                if (root.response[i].continent == null)
                {
                    ListContinent[i] = "None";
                }
                else
                {
                    ListContinent[i] = root.response[i].continent;
                }
            }
        }
        //Getter
        public string[] getListTotalTests()
        {
            return ListTotalTests;
        }

        public string[] getTestsM_pop()
        {
            return TestsM_pop;
        }

        public string[] getListTotalDeaths()
        {
            return ListTotalDeaths;
        }

        public string[] getDeathsM_pop()
        {
            return DeathsM_pop;
        }

        public string[] getListNewDeaths()
        {
            return ListNewDeaths;
        }

        public string[] getListTotalCases()
        {
            return ListTotalCases;
        }

        public string[] getCasesM_pop()
        {
            return CasesM_pop;
        }

        public string[] getListRecoveredCases()
        {
            return ListActiveCases;
        }

        public string[] getListCriticalCases()
        {
            return ListActiveCases;
        }

        public string[] getListActiveCases()
        {
            return ListActiveCases;
        }
        public string[] getListNewCases()
        {
            return ListNewCases;
        }
        public string[] getListTime()
        {
            return ListTime;
        }
        public string[] getListDay()
        {
            return ListDay;
        }

        public string[] getListPopulation()
        {
            return ListPopulation;
        }

        public string[] getListCountry()
        {
            return ListCountry;
        }

        public string[] getListContinent()
        {
            return ListContinent;
        }
    }
}