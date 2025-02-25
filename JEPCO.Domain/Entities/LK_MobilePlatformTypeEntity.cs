using JEPCO.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEPCO.Domain.Entities
{
    [Table("lk_mobile_platform_types")]

    public class LK_MobilePlatformTypeEntity : BaseLookupEntity
    {
        public virtual ICollection<UserCloudMessagingTokenEntity>? UserCloudMessagingTokens { get; set; }
    }
}
