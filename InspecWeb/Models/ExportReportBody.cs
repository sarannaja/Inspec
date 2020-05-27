using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{

    [Table("ExportReportBody")]
    [Description("ตาราง Export ทะเบียนรายงาน")]
    public class ExportReportBody
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("ExportReportHead")]
        [Description("FK: จังหวัด")]
        public long ExportReportHeadId { get; set; }
        public virtual ExportReportHead ExportReportHead { get; set; }
        public string Subject { get; set; }
        public string Problem { get; set; }
        public string Department { get; set; }
        public string Suggestion { get; set; }
        public string Report { get; set; }
        public string File { get; set; }
        public string Comment { get; set; }
    }
}
