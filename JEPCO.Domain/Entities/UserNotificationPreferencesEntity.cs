using JEPCO.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEPCO.Domain.Entities
{
    [Table("user_notification_preferences")]
    public class UserNotificationPreferencesEntity : BaseEntity, IAuditLoggableEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(UserEntity.NotificationPreferences))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public UserEntity? User { get; set; }

        public int NotificationTypeId { get; set; }

        [ForeignKey(nameof(NotificationTypeId))]
        [InverseProperty(nameof(LK_NotificationTypeEntity.UserNotificationPreferences))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public LK_NotificationTypeEntity? NotificationType { get; set; }

        public bool IsActive { get; set; }

    }
}
