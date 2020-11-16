using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace InspecWeb.Models
{
    /// <summary>
    /// เนื้อหาของสื่อสิ่งพิมพ์
    /// </summary>
    [Table("Cabines")]
    [Description("ตารางคณะรัฐมนตรี")]
    public class Cabine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("คำนำหน้า")]
        public string Prefix { get; set; }

        [Required]
        [Description("ชื่อ-นามสกุล")]
        public string Name { get; set; }

        [Required]
        [Description("ตำแหน่ง")]
        public string Position { get; set; }

        [Required]
        [Description("รูปภาพ")]
        public string Image { get; set; }

   
        [Description("ที่อยู่")]
        public string Detail { get; set; }

   
        [Description("เบอร์โทร")]
        public string tel { get; set; }

     
        [Description("คำสั่ง")]
        public string Commandnumber { get; set; }

       
        [Description("ครม.")]
        public string cabinet { get; set; }

        [ForeignKey("Ministry")]
        [Description("FK: กระทรวง")]
        public long MinistryId { get; set; }
        public virtual Ministry Ministries { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

    }
}