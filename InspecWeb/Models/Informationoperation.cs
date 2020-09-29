using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("Informationoperations")]
    [Description("ข้อมูลสำหรับการปฏิบัติงานของเจ้าหน้าที่ประจำเขตตรวจราชการ อาทิ ข้อมูลการติดต่อที่พัก ยานพาหนะ และร้านอาหาร เที่ยวบิน")]
    public class Informationoperation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("ที่พัก,ร้านอาหาร")]
        public string Location { get; set; }

        [Required]
        [Description("ชื่อ")]
        public string Name { get; set; }

        [Required]
        [Description("รายละเอียด")]
        public string Detail { get; set; }

        [Required]
        [Description("เบอร์")]
        public string Tel { get; set; }

        [Required]
        [Description("จังหวัด")]
        public string Province { get; set; }

        [Required]
        [Description("อำเภอ")]
        public string District { get; set; }

        [Required]
        [Description("ชื่อไฟล์")]
        public string File { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }


    }
}
