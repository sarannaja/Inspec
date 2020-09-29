namespace InspecWeb.ViewModel
{
    public class WordViewModel
    {

        public long id { get; set; }
        //public long ProvinId { get; set; }
        public long elecId { get; set; }

    }

    public class WordsubjectViewModel
    {

        public string Name { get; set; }
        public string Detail { get; set; }
        public string Problem { get; set; }
        public string Suggestion { get; set; }

    }

    public class WordfileViewModel
    {

        public string Name { get; set; }
        public string Description { get; set; }

    }



}
