using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidDataApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ParseData covid=new ParseData();
            covid.ParseJSON();
            string[] Countries=covid.getListCountry();
            string[] Continents=covid.getListContinent();
            string[] Populations = covid.getListPopulation();
            string[] Days = covid.getListDay();
            string[] Times=covid.getListTime();

            string[] NewCases=covid.getListNewCases();
            string[] ActiveCases = covid.getListActiveCases();
            string[] CriticalCases = covid.getListCriticalCases();
            string[] RecoveredCases = covid.getListRecoveredCases();
            string[] CasesM_pop = covid.getCasesM_pop();
            string[] TotalCases = covid.getListTotalCases();
            //Deaths
            string[] NewDeaths = covid.getListNewDeaths();
            string[] DeathsM_pop = covid.getDeathsM_pop();
            string[] TotalDeaths = covid.getListTotalDeaths();
            //Tests
            string[] TestsM_pop = covid.getTestsM_pop();
            string[] TotalTests = covid.getListTotalTests();

            int size= Countries.Length;
            string[] lines=new string[size];
            for (int i = 0; i < size; i++)
            {
                int index = i + 1;
                //lines[i]= $"| {Countries[i].PadRight(22)}| {Continents[i].PadRight(14)}| {Populations[i].PadRight(10)}| {NewCases[i].PadRight(7)}| {ActiveCases[i].PadRight(9)}| {CriticalCases[i].PadRight(7)}| {RecoveredCases[i].PadRight(7)}| {CasesM_pop[i].PadRight(7)}|{TotalCases[i].PadRight(7)}| {NewDeaths[i].PadRight(5)}| {DeathsM_pop[i].PadRight(7)}| {TotalDeaths[i].PadRight(6)}| {TestsM_pop[i].PadRight(7)}| {TotalTests[i].PadRight(10)}|{Days[i].PadRight(10)}|";
                lines[i] = $"|{index.ToString().PadRight(3)}| {Countries[i].PadRight(22)}| {Continents[i].PadRight(14)}| {Populations[i].PadRight(10)}| {NewCases[i].PadRight(7)}| {ActiveCases[i].PadRight(10)}| {CriticalCases[i].PadRight(8)}| {RecoveredCases[i].PadRight(9)}|{TotalCases[i].PadRight(10)}| {NewDeaths[i].PadRight(7)}| {TotalDeaths[i].PadRight(9)}| {TotalTests[i].PadRight(10)}| {Days[i].PadRight(11)}|";

            }

            Console.WriteLine($"查询时间：{DateTime.Now} 数据来源：covid-193.p.rapidapi.com");
            //Console.WriteLine($"|国家                   |所属洲         |人口       |新增病例|当前病例  |当前重症|已康复  |每百万人口患病数|历史总病例 |新增死亡|每百万人死亡病数           |历史总死亡          |每百万人口检测数        |总检测数          |上次更新时间         |");
            Console.WriteLine($"|   |国家/地区              |所属洲         |人口       |新增病例|当前病例   |当前重症 |已康复    |历史总病例|新增死亡|历史总死亡|总检测数   |上次更新时间|");
            foreach (string line in lines) 
            { 
                Console.WriteLine($"{line}");
            }                    
            Console.ReadKey();
        }
    }
}
