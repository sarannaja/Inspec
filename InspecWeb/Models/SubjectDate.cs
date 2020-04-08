using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("SubjectDates")]
    [Description("ตารางประเด็นเวลา")]
    public class SubjectDate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("Subject")]
        [Description("FK: ประเด็น")]
        public long SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        [ForeignKey("CentralPolicyDate")]
        [Description("FK: ปีงบประมาณ")]
        public long CentralPolicyDateId { get; set; }

        public virtual CentralPolicyDate CentralPolicyDate { get; set; }
    }
}
