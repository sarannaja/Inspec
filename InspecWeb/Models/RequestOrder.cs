﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    /// <summary>
    /// เนื้อหาของสื่อสิ่งพิมพ์
    /// </summary>
    [Table("RequestOrders")]
    [Description("ตารางแจ้งคำร้องขอ")]
    public class RequestOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("คำร้องขอ")]
        public string DetailRequestOrder { get; set; }

        [Description("รายละเอียด")]
        public string AnswerDetail { get; set; }

        [Description("ปัญหา/อุปสรรค")]
        public string AnswerProblem { get; set; }

        [Description("ข้อเสนอแนะ")]
        public string AnswerCounsel { get; set; }

        [ForeignKey("CentralPolicy")]
        [Description("FK: แผนการตรวจ")]
        public long CentralPolicyId { get; set; }

        public virtual CentralPolicy CentralPolicy { get; set; }

        [ForeignKey("ApplicationUser")]
        [Description("FK: ผู้แจ้งคำร้องขอ")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Description("FK: ผู้รับคำร้องขอ")]
        public string AnswerUserId { get; set; }

        [ForeignKey("Province")]
        [Description("FK: จังหวัด")]
        public long ProvinceId { get; set; }
        public virtual Province Province { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        public ICollection<RequestOrderFile> RequestOrderFiles { get; set; }
        public ICollection<AnswerRequestOrderFile> AnswerRequestOrderFile { get; set; }


    }
}