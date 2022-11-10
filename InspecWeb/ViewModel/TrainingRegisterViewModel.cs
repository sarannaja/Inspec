using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class TrainingRegisterViewModel
    {
        public long departmentid { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public long trainingid { get; set; }
        public string phone { get; set; }
        public string cardid { get; set; }
        public string position { get; set; }
        public string PositionBefore { get; set; }
        public long status { get; set; }

        public string PassportStatus { get; set; }
        public DateTime PassportExpire { get; set; }
        public string Address { get; set; }
        public string OfficePhone { get; set; }
        public string Religion { get; set; }
        public string Food { get; set; }
        public string Foodallergy { get; set; }
        public string Blood { get; set; }
        public string CongenitalDisease { get; set; }
        public string CollaboratorPosition { get; set; }

        public string userid { get; set; }
        public long usertype { get; set; }
        public string email { get; set; }
        public string department { get; set; }
        public List<IFormFile> files { get; set; }
        public List<IFormFile> CertificationFiles { get; set; }
        public List<IFormFile> idcardFiles { get; set; }
        public List<IFormFile> GovernmentpassportFiles { get; set; }

        public string type { get; set; }
        public string nickname { get; set; }
        public DateTime retireddate { get; set; }
        public DateTime birthdate { get; set; }
        public string officeaddress { get; set; }
        public string fax { get; set; }
        public string collaboratorname { get; set; }
        public string collaboratorphone { get; set; }
        public string collaboratorphoneoffice { get; set; }
        public string collaboratoremail { get; set; }
    }
}
