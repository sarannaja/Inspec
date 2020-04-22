using System;
using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class ElectronicBookViewModel
    {
        public string Detail { get; set; }
        public Inputelectronicbook[] Inputelectronicbook { get; set;}
        public UserPeopleId[] UserPeopleId { get; set; }
        public UserMinistryId[] UserMinistryId { get; set; }
    }

    public class Inputelectronicbook
    {
        public long ProvinceId { get; set; }
        public long CentralPolicyId { get; set; }
    }

    public class UserPeopleId
    {
        public string Id { get; set; }
    }
    public class UserMinistryId
    {
        public string Id { get; set; }
    }
}
