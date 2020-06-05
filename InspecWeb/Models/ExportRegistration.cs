using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{

    [Table("ExportRegistration")]
    [Description("ตาราง Export ทะเบียนรายงาน")]
    public class ExportRegistration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("Province")]
        [Description("FK: จังหวัด")]
        public long ProvinceId { get; set; }
        public virtual Province Province { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
        public string Subject { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime? SendDate { get; set; }
        public string ExcutiveOerder { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExcutiveOerderDate { get; set; }
        public string File { get; set; }
    }
}
