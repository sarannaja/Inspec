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
    [Table("RequestOrders")]
    [Description("ตารางแจ้งคำร้องขอ")]
    public class RequestOrder
    {
    
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("ApplicationUser")]
        [Description("FK: User")]
        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        [Description("ประเด็น/เรื่อง")]
        public string Subject { get; set; }

        [Required]
        [Description("รายละเอียดประเด็น/เรื่อง")]
        public string Subjectdetail { get; set; }

        [Description("วันที่มีคำร้อง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        [Description("วันที่มีคำร้อง")]
        [DataType(DataType.Date)]
        public DateTime? Commanded_date { get; set; }

        [Description("publics")]
        public long publics { get; set; }

        [Description("Draft")]
        public long Draft { get; set; }

        [Description("Accept")]
        public long Accept { get; set; }

        [Description("cancel")]
        public long Cancel { get; set; }

        [Description("canceldetail")]
        public string Canceldetail { get; set; }

        public ICollection<RequestOrderFile> RequestOrderFiles { get; set; }

        public ICollection<RequestOrderAnswer> RequestOrderAnswers { get; set; }
    }
}