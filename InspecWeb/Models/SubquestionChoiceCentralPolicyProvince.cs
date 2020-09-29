using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("SubquestionChoiceCentralPolicyProvinces")]
    [Description("ตารางประเด็น")]
    public class SubquestionChoiceCentralPolicyProvince
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("SubquestionCentralPolicyProvince")]
        [Description("FK: คำถามย่อย")]
        public long SubquestionCentralPolicyProvinceId { get; set; }

        public virtual SubquestionCentralPolicyProvince SubquestionCentralPolicyProvince { get; set; }

        [Required]
        [Description("ชื่อตัวเลืือก")]
        public string Name { get; set; }


    }
}
