﻿using System;
using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class QuestioncloseViewModel
    {

        public long Id { get; set; }


        public long SubjectId { get; set; }


        public string Name { get; set; }


        public inputanswerclose2[] inputanswerclose2 { get; set; }

    }

    public class inputanswerclose2
    {
        public string answerclose { get; set; }
    }
}