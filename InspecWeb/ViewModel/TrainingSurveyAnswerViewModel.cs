using System;
using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class TrainingSurveyAnswerViewModel
    {

        public string Username { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public long TrainingLecturerJoinSurveyId { get; set; }



        public inputtrainingsurveyanswer[] inputtrainingsurveyanswer { get; set; }

       
    }

    public class inputtrainingsurveyanswer
    {
        public long trainingsurveyId { get; set; }
        public int score { get; set; }
        public int SurveyType { get; set; }
        public string ansText { get; set; }
        public int ansYesOrNo { get; set; }



    }


}
