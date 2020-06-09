using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("Premierorders")]
    [Description("กฎหมาย ระเบียบ หนังสือเวียนต่าง ๆ อาทิ ระเบียบสำนักนายกรัฐมนตรีว่าด้วยการตรวจราชการ พ.ศ.2551 ระเบียบสำนักนายกรัฐมนตรีว่าด้วยคณะกรรมการ ธรรมาภิบาลจังหวัด พ.ศ. 2552 และที่แก้ไขเพิ่มเติม ระเบียบสำนักนายกรัฐมนตรีว่าด้วยการกำกับติดตามการปฏิบัติราชการในภูมิภาค")]
    public class Premierorder
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
