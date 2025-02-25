using JEPCO.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEPCO.Domain.Entities
{
    [Table("lk_languages")]
    public class LK_LanguageEntity : BaseLookupEntity
    {
        public virtual ICollection<UserEntity>? Users { get; set; }
    }
}
