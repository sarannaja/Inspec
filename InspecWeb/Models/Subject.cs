using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("Subjects")]
    [Description("ตารางประเด็น")]
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("CentralPolicy")]
        [Description("FK: นโยบายกลาง")]
        public long CentralPolicyId { get; set; }

        public virtual CentralPolicy CentralPolicy { get; set; }

       
        [Description("ชื่อประเด็น")]
        public string Name { get; set; }

        
        [Description("คำตอบของประเด็น")]
        public string Answer { get; set; }

        //[Description("วันที่เริ่ม")]
        //[DataType(DataType.Date)]
        //public DateTime StartDate { get; set; }

        //[Description("วันที่สิ้นสุด")]
        //[DataType(DataType.Date)]
        //public DateTime EndDate { get; set; }

        // public ICollection<SubjectDate> SubjectDates { get; set; }
        public ICollection<Subquestion> Subquestions { get; set; }
    }
}
