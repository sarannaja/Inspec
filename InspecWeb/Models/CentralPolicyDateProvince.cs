using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("CentralPolicyDateProvinces")]
    [Description("ตารางไฟล์นโยบาลกลาง")]
    public class CentralPolicyDateProvince
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Description("วันที่เริ่ม")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Description("วันที่สิ้นสุด")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

    }
}
