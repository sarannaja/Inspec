using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    public class UserRegion
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("ApplicationUser")]
        [Description("FK: User")]
        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }

        [ForeignKey("Region")]
        [Description("FK: เขตตรวจ")]
        public long RegionId { get; set; }

        public virtual Region Region { get; set; }
    }
    public class UserRegionArray
    {
        public UserRegion[] userregion { get; set; }
    }

}
