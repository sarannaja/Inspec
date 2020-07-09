using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Collections;

namespace InspecWeb.ViewModel
{
    public class TrainingViewModel
    {

        public string Name { get; set; }

        public string Detail { get; set; }

        public int Generation { get; set; }

        public int Year { get; set; }

        public int CourseCode { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime RegisStartDate { get; set; }

        public DateTime RegisEndDate { get; set; }

        public List<IFormFile> files { get; set; }

    }

    public class TrainingphaseViewModel
    {
        public long Id { get; set; }

        public long TrainingId { get; set; }

        public long PhaseNo { get; set; }

        public string Title { get; set; }

        public string Detail { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Location { get; set; }

        public long Group { get; set; }
    }

    public class TrainingProgramViewModel
    {
        public long Id { get; set; }

        public long TrainingPhaseId { get; set; }

        public DateTime ProgramDate { get; set; }

        public string MinuteStartDate { get; set; }

        public string MinuteEndDate { get; set; }

        public string ProgramType { get; set; }

        public string ProgramTopic { get; set; }

        public string ProgramDetail { get; set; }

        public string ProgramLocation { get; set; }

        public string ProgramToDress { get; set; }

        public long[] TrainingLecturerId { get; set; }

        public List<IFormFile> files { get; set; }
    }

}
