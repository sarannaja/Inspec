using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class ElectronicBookViewModel
    {
        [JsonProperty(PropertyName = "Detail")]
        public string Detail { get; set; }
        //public Inputelectronicbook[] Inputelectronicbook { get; set;}
        public long CentralPolicyId { get; set; }
        public long ProvinceId { get; set; }
        //public UserPeopleId[] UserPeopleId { get; set; }
        //public UserMinistryId[] UserMinistryId { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        [JsonProperty(PropertyName = "Status")]
        public string Status { get; set; }
        public List<IFormFile> files { get; set; }

        public string[] UserPeopleId { get; set; }
        public string[] UserMinistryId { get; set; }
        public long[] ProvincialDepartmentId { get; set; }
    }

    //public class Inputelectronicbook
    //{
    //    public long ProvinceId { get; set; }
    //    public long CentralPolicyId { get; set; }
    //}

    //public class UserPeopleId
    //{
    //    public string Id { get; set; }
    //}
    //public class UserMinistryId
    //{
    //    public string Id { get; set; }
    //}
}
