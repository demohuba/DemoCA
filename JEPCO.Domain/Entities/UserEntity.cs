using JEPCO.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEPCO.Domain.Entities
{
    [Table("users")]
    public class UserEntity : DeletableBaseEntity, IAuditLoggableEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string MobileNumber { get; set; } = string.Empty;
        public string Email { get; set; }
        public string UserName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public int GenderId { get; set; }

        [ForeignKey(nameof(GenderId))]
        [InverseProperty(nameof(LK_GenderEntity.Users))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public LK_GenderEntity? Gender { get; set; }

        public int PreferredLanguageId { get; set; }

        [ForeignKey(nameof(PreferredLanguageId))]
        [InverseProperty(nameof(LK_LanguageEntity.Users))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public LK_LanguageEntity? PreferredLanguage { get; set; }

        public int UserTypeId { get; set; }

        [ForeignKey(nameof(UserTypeId))]
        [InverseProperty(nameof(LK_UserTypeEntity.Users))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public LK_UserTypeEntity? UserType { get; set; }

        [InverseProperty(nameof(UserNotificationPreferencesEntity.User))]
        public virtual ICollection<UserNotificationPreferencesEntity>? NotificationPreferences { get; set; }
        [InverseProperty(nameof(UserCloudMessagingTokenEntity.User))]
        public virtual ICollection<UserCloudMessagingTokenEntity>? CloudMessagingTokens { get; set; }

        [InverseProperty(nameof(CustomerEntity.User))]
        public virtual CustomerEntity? Customer { get; set; }
    }
}
