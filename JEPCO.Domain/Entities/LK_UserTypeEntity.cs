using JEPCO.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEPCO.Domain.Entities
{
    [Table("lk_user_types")]

    public class LK_UserTypeEntity : BaseLookupEntity
    {
        public virtual ICollection<UserEntity>? Users { get; set; }
    }
}
