using JEPCO.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEPCO.Domain.Entities
{
    [Table("lk_genders")]
    public class LK_GenderEntity : BaseLookupEntity
    {
        public virtual ICollection<UserEntity>? Users { get; set; }
    }
}
