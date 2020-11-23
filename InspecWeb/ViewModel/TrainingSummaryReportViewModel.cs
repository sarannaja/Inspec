using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Collections;
using InspecWeb.Models;

namespace InspecWeb.ViewModel
{
    public class TrainingSummaryReportGroupViewModel
    {
        public long TrainingPhaseId { get; set; }
        //public long Group { get; set; }
        public string Detail { get; set; }

        public List<IFormFile> files { get; set; }


    }

    public class TrainingSummaryReportProjectViewModel
    {
        public long TrainingId { get; set; }
        //public long Group { get; set; }
        public string Detail { get; set; }

        public List<IFormFile> files { get; set; }


    }

    public class GetTrainingSurveyLecturerListViewModel
    {
        public long TrainingId { get; set; }
        //public long Group { get; set; }
        public string TrainingName { get; set; }

        public List<TrainingLecturerJoinSurvey> TrainingLecturerJoinSurveys { get; set; }
        
        public int AnsCount { get; set; }


    }


}
