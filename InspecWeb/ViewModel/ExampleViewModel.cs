using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace InspecWeb.ViewModel
{

    public class DataExample
    {
        //string
        public string name { get; set; }
        //รับค่า MemberWorkViewM เป็น array [{subjects:""}]
        public List<MemberWorkViewM> array { get; set; }

        //รับค่า object ที่สร้า่งในประกอบไปด้วยตัวแแปรใน MemberWorkViewM {subjects:""}
        public MemberWorkViewM obj { get; set; }
        //รับไฟล์มาเป็น array
        public List<IFormFile> files { get; set; }


    }

    public class MemberWorkViewM
    {
        public string name { get; set; }
        public string subjects { get; set; }

    }

    public class test
    {

    }
}

