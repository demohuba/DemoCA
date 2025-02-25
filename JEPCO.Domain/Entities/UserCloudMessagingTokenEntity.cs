using JEPCO.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEPCO.Domain.Entities
{
    [Table("user_cloud_messaging_tokens")]
    public class UserCloudMessagingTokenEntity : DeletableBaseEntity, IAuditLoggableEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(UserEntity.CloudMessagingTokens))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual UserEntity? User { get; set; }
        public string TokenId { get; set; } = string.Empty;
        public int MobilePlatformId { get; set; }

        [ForeignKey(nameof(MobilePlatformId))]
        [InverseProperty(nameof(LK_MobilePlatformTypeEntity.UserCloudMessagingTokens))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual LK_MobilePlatformTypeEntity? MobilePlatform { get; set; }
        public string? DeviceName { get; set; }
    }
}
