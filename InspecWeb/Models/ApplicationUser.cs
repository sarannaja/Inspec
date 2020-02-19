using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Description("สิทธิ์การใช้งาน")]
        public long Role_id { get; set; }

        [Description("จังหวัดภายใต้")]
        public long Province_id { get; set; }

        [Description("กระทรวง")]
        public long Ministry_id { get; set; }

        [Description("กรม/หน่วยงาน")]
        public long Department_id { get; set; }

        [Description("เขตตรวจราชการ")]
        public long Region_id { get; set; }

        [Description("รูปโปรไฟล์")]
        public long Profile { get; set; }
    }
}
