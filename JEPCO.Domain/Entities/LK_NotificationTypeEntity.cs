using JEPCO.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEPCO.Domain.Entities
{
    [Table("lk_notification_types")]

    public class LK_NotificationTypeEntity : BaseLookupEntity
    {
        public virtual ICollection<UserNotificationPreferencesEntity>? UserNotificationPreferences { get; set; }
    }
}
