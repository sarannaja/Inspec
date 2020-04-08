using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("SubquestionChoices")]
    [Description("ตารางประเด็น")]
    public class SubquestionChoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("Subquestion")]
        [Description("FK: คำถามย่อย")]
        public long SubquestionId { get; set; }

        public virtual Subquestion Subquestion { get; set; }

        [Required]
        [Description("ชื่อตัวเลืือก")]
        public string Name { get; set; }


    }
}
