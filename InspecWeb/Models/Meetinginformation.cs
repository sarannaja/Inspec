using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("Meetinginformations")]
    [Description("ข้อมูลเกี่ยวกับการประชุมต่าง ๆ อาทิ หนังสือเชิญประชุม ระเบียบวาระการประชุม รายงานการประชุม เอกสารประกอบการประชุม")]
    public class Meetinginformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("ปี")]
        public string Year { get; set; }

        [Required]
        [Description("หัวเรื่อง")]
        public string Title { get; set; }

        [Required]
        [Description("ชื่อไฟล์")]
        public string File { get; set; }

    }
}
