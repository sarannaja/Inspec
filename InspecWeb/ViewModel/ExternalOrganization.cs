using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Refit;

namespace InspecWeb.ViewModel
{
    public class ExternalOrganization
    {

    }

    public interface IHelloClient
    {
        [Get("/ggcservice")]
        List<Ggc> GetMessageAsync();
    }

    public class Ggc
    {
        public string fullname { get; set; }
        public string appointment { get; set; }
        public string address { get; set; }
        public string phonenumber { get; set; }
    }


    public class OtpsMinisters
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Position { get; set; }
        public string PositionShort { get; set; }
        public OtpsCabinets Cabinet { get; set; }
        public List<OtpsFiscalYearMinisters> FiscalYears { get; set; }

    }


    public class OtpsRegions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<OtpsFiscalYears> FiscalYears { get; set; }

    }

    public class OtpsFiscalYears
    {
        public int Year { get; set; }
        public string Name { get; set; }
        public OtpsProjects Projects { get; set; }

        public List<OtpsProvinces> Provinces { get; set; }

    }
    public class OtpsProjects
    {
        public int Count { get; set; }
        public int Completed { get; set; }
        public double TotalBudget { get; set; }
        public double TotalAmount { get; set; }
        public double TotalSpent { get; set; }
        public double TotalPercent { get; set; }

    }
    public class OtpsProvinces
    {

        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class OtpsCabinets
    {

        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class OtpsFiscalYearMinisters
    {

        public int Year { get; set; }
        public string Name { get; set; }
        public OtpsProjects Projects { get; set; }

        public List<OtpsRegionMinisters> Regions { get; set; }

    }

    public class OtpsRegionMinisters
    {

        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class Cabinets
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<OtpsCabinetFiscalYears> FiscalYears {get;set;}
        public List<OtpsCabinetMinisters> Ministers   {get;set;}
    }

    public class OtpsCabinetFiscalYears
    {
        public int Year { get; set; }
        public string Name { get; set; }
        public OtpsProjects Projects { get; set; }

        // public List<OtpsProvinces> Provinces { get; set; }

    }

    public class OtpsCabinetMinisters
    {
        public int Year { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string PositionShort { get; set; }
        // public OtpsProjects Projects { get; set; }

        // public List<OtpsProvinces> Provinces { get; set; }

    }

}
