using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    /// <summary>
    /// เนื้อหาของสื่อสิ่งพิมพ์
    /// </summary>
    [Table("TrainingRegisterFiles")]
    [Description("ตารางจัดกลุ่มผู้สมัครตามช่วงแผนกิจกรรม")]
    public class TrainingRegisterFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("TrainingRegister")]
        [Description("FK: ผู้สมัคร")]
        public long RegisterId { get; set; }
        public virtual TrainingRegister TrainingRegister { get; set; }
        public string Name { get; set; }

        [Description("กลุ่ม 1")]
        public string Type { get; set; }

    }
}