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
            ParseData covid = new ParseData();
            covid.ParseJSON();
            string[] Countries = covid.getListCountry();
            string[] Continents = covid.getListContinent();
            string[] Populations = covid.getListPopulation();
            string[] Days = covid.getListDay();
            string[] Times = covid.getListTime();

            string[] NewCases = covid.getListNewCases();
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

            int size = Countries.Length;

            #region 控制台与文本文档内容
            string[] lines = new string[size];

            for (int i = 0; i < size; i++)
            {
                int index = i + 1;
                //lines[i]= $"| {Countries[i].PadRight(22)}| {Continents[i].PadRight(14)}| {Populations[i].PadRight(10)}| {NewCases[i].PadRight(7)}| {ActiveCases[i].PadRight(9)}| {CriticalCases[i].PadRight(7)}| {RecoveredCases[i].PadRight(7)}| {CasesM_pop[i].PadRight(7)}|{TotalCases[i].PadRight(7)}| {NewDeaths[i].PadRight(5)}| {DeathsM_pop[i].PadRight(7)}| {TotalDeaths[i].PadRight(6)}| {TestsM_pop[i].PadRight(7)}| {TotalTests[i].PadRight(10)}|{Days[i].PadRight(10)}|";
                lines[i] = $"|{index.ToString().PadRight(3)}| {Countries[i].PadRight(22)}| {Continents[i].PadRight(14)}| {Populations[i].PadRight(10)}| {NewCases[i].PadRight(7)}| {ActiveCases[i].PadRight(10)}| {CriticalCases[i].PadRight(8)}| {RecoveredCases[i].PadRight(9)}|{TotalCases[i].PadRight(10)}| {NewDeaths[i].PadRight(7)}| {TotalDeaths[i].PadRight(9)}| {TotalTests[i].PadRight(10)}| {Days[i].PadRight(11)}|";

            }
            string basic = $"查询时间：{DateTime.Now} 数据来源：covid-193.p.rapidapi.com";
            Console.WriteLine(basic);
            string header = $"|   |国家/地区              |所属洲         |人口       |新增病例|当前病例   |当前重症 |已康复    |历史总病例|新增死亡|历史总死亡|总检测数   |上次更新时间|";
            //string header =$"|国家                   |所属洲         |人口       |新增病例|当前病例  |当前重症|已康复  |每百万人口患病数|历史总病例 |新增死亡|每百万人死亡病数           |历史总死亡          |每百万人口检测数        |总检测数          |上次更新时间         |";
            Console.WriteLine(header);
            string body = "";
            foreach (string line in lines)
            {
                body = body + line + "\n";
                Console.WriteLine($"{line}");
            }
            string txtContent = basic + "\n" + header + "\n" + body;
            //Console.WriteLine(txtContent);
            WriteInFile.WriteContentInTxt(txtContent);

            #endregion

            #region
            string[,] table = new string[size + 1, 13];

            for (int i = 0; i <= size - 1; i++)
            {
                if (i == 0)
                {
                    table[i, 0] = "#";
                    table[i, 1] = "国家/地区";
                    table[i, 2] = "所属洲";
                    table[i, 3] = "人口 ";
                    table[i, 4] = "新增病例";
                    table[i, 5] = "当前病例";
                    table[i, 6] = "当前重症";
                    table[i, 7] = "已康复";
                    table[i, 8] = "历史总病例";
                    table[i, 9] = "新增死亡";
                    table[i, 10] = "历史总死亡";
                    table[i, 11] = "总检测数";
                    table[i, 12] = "上次更新时间";
                }
                else
                {
                    table[i, 0] = i.ToString();
                    table[i, 1] = Countries[i - 1];
                    table[i, 2] = Continents[i - 1];
                    table[i, 3] = Populations[i - 1];
                    table[i, 4] = NewCases[i - 1];
                    table[i, 5] = ActiveCases[i - 1];
                    table[i, 6] = CriticalCases[i - 1];
                    table[i, 7] = RecoveredCases[i - 1];
                    table[i, 8] = TotalCases[i - 1];
                    table[i, 9] = NewDeaths[i - 1];
                    table[i, 10] = TotalDeaths[i - 1];
                    table[i, 11] = TotalTests[i - 1];
                    table[i, 12] = Days[i - 1];
                }

            }

            #endregion
            WriteInFile.WriteContentInDoc(basic, table);
            WriteInFile.WriteContentInExcel(basic, table);


            Console.ReadKey();
        }
    }
}
