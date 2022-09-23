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

        public string Generation { get; set; }

        public string Year { get; set; }

        public string CourseCode { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime RegisStartDate { get; set; }

        public DateTime RegisEndDate { get; set; }

        public List<IFormFile> files { get; set; }

        public long[] RegisterId { get; set; }

        public TrainingCode[] TrainingCode { get; set; }
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

        public long ProgramType { get; set; }

        public string ProgramTopic { get; set; }

        public string ProgramDetail { get; set; }

        public string ProgramLocation { get; set; }

        public string ProgramToDress { get; set; }

        public long[] TrainingLecturerId { get; set; }

        public List<IFormFile> files { get; set; }

        public long[] RemoveLecturer { get; set; }

        public long[] AddLecturer { get; set; }
    }

    public class TrainingLoginViewModel
    {
        public string username { get; set; }

        public long trainingPhaseId { get; set; }

        public DateTime registerDate { get; set; }

        public long trainingProgramLoginId { get; set; }

        public long dateType { get; set; }

        public long trainingid { get; set; }
    }

    public class TrainingCode
    {
        public long id { get; set; }

        public string code { get; set; }

    }


    public class TrainingLecturerViewModel
    {

        public long LecturerType { get; set; }

        public string LecturerName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Education { get; set; }

        public string WorkHistory { get; set; }

        public string Experience { get; set; }

        public string DetailPlus { get; set; }

        public string Address { get; set; }

        public List<IFormFile> ImageProfile { get; set; }

        public List<IFormFile>  ProfileUploads { get; set; }
    }
}
