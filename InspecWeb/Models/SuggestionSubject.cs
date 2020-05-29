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
    [Table("SuggestionSubject")]
    [Description("ตารางแสดงความคิดเห็น")]
    public class SuggestionSubject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }


        [ForeignKey("SubjectCentralPolicyProvince")]
        [Description("FK: ประเด็น")]
        public long SubjectCentralPolicyProvinceId { get; set; }

        public virtual SubjectCentralPolicyProvince SubjectCentralPolicyProvince { get; set; }

        [ForeignKey("User")]
        [Description("FK: User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        [Description("ความคิดเห็น")]
        public string Suggestion { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }
    }
}