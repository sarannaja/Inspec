using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("Subquestions")]
    [Description("ตารางประเด็น")]
    public class Subquestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("Subject")]
        [Description("FK: ประเด็น")]
        public long SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        [Required]
        [Description("ชื่อคำถามย่อย")]
        public string Name { get; set; }

        [Required]
        [Description("ประเภทของคำถามย่อย")]
        public string Type { get; set; }

        public ICollection<SubquestionChoice> SubquestionChoices { get; set; }
    }
}
