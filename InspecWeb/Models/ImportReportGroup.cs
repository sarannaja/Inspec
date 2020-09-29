using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("ImportReportGroups")]
    [Description("ตารางเชื่อมนโยบาลกลาง")]
    public class ImportReportGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("ImportReport")]
        [Description("FK: ImportReport")]
        public long ImportReportId { get; set; }
        public virtual ImportReport ImportReport { get; set; }

        [ForeignKey("CentralPolicyEvent")]
        [Description("FK: CentralPolicyEventId")]
        public long CentralPolicyEventId { get; set; }
        public virtual CentralPolicyEvent CentralPolicyEvent { get; set; }
    }
}
