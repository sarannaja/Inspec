using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Collections;

namespace InspecWeb.ViewModel
{
    public class RegisterConditionViewModel
    {
        public long traningregisterid { get; set; }
        public traningregistercondition[] traningregistercondition { get; set; }

    }

    public class traningregistercondition
    {
        public long id { get; set; }
        public Boolean status { get; set; }
    }
}
