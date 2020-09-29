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
        public bool status { get; set; }
    }
}
