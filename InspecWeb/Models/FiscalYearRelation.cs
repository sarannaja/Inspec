using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("FiscalYearRelations")]
    [Description("ตารางความสัมพัธ์ระหว่างปีและเขตตรวจ")]
    public class FiscalYearRelation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("FiscalYear")]
        [Description("FK: ปีงบประมาณ")]
        public long FiscalYearId { get; set; }

        public virtual FiscalYear FiscalYear { get; set; }

        [ForeignKey("Region")]
        [Description("FK: เขตตรวจราขการ")]
        public long RegionId { get; set; }
        public virtual Region Region { get; set; }

        [ForeignKey("Province")]
        [Description("FK: จังหวัด")]
        public long ProvinceId { get; set; }

        public virtual Province Province { get; set; }
    }
}
