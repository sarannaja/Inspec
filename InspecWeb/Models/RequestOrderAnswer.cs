using System;
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
    [Table("RequestOrderAnswers")]
    [Description("ตารางแจ้งคำร้องขอ")]
    public class RequestOrderAnswer
    {
    
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("RequestOrder")]
        [Description("FK: ข้อสั่งการ")]
        public long RequestOrderId { get; set; }

        public virtual RequestOrder RequestOrder { get; set; }

        [Required]
        [Description("สถานะ")]
        public string Status { get; set; }

        [ForeignKey("ApplicationUser")]
        [Description("FK: User")]
        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Description("วันที่รับทราบข้อสั่งการ")]
        [DataType(DataType.Date)]
        public DateTime? beaware_date { get; set; }

        [Description("publics")]
        public long publics { get; set; }

        public ICollection<RequestOrderAnswerDetail> RequestOrderAnswerDetails { get; set; }

    }
}