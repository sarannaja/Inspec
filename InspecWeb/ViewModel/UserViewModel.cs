using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspecWeb.ViewModel
{
    public class UserViewModel
    {
        //23
        public string UserName { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public long MinistryId { get; set; }
        public long DepartmentId { get; set; }
        public long ProvincialDepartmentId { get; set; } //21-06-2020
        public long DistrictId { get; set; }
        public long ProvinceId { get; set; }
        public long SubdistrictId { get; set; }
        public string Prefix { get; set; }
        public long Role_id { get; set; }
        public string Educational { get; set; }
        public DateTime? Birthday { get; set; }
        public string Officephonenumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Telegraphnumber { get; set; }
        public string Housenumber { get; set; }
        public string Rold { get; set; }
        public string Alley { get; set; }
        public string Postalcode { get; set; }
        public string Side { get; set; }
        public string Img { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public long Active { get; set; }
        public String Signature { get; set; }
        public string Commandnumber { get; set; }
        public long Formprofile { get; set; }
        public List<IFormFile> files { get; set; }

        public List<int> UserRegion { get; set; } //สำหรับทำ array

        public List<int> UserProvince { get; set; } //สำหรับทำ array

        public long UserProvinceId { get; set; }

        public long UserRegionId { get; set; } //สำหรับทำ array
    }

    public class UserArray
    {
        public UserViewModel[] user { get; set; }
    }
}
