using JEPCO.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEPCO.Domain.Entities
{
    [Table("customers")]
    public class CustomerEntity : DeletableBaseEntity, IAuditLoggableEntity
    {
        [Key]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual UserEntity? User { get; set; }

        [InverseProperty(nameof(CustomerMeterEntity.Customer))]
        public virtual ICollection<CustomerMeterEntity>? CustomerMeters { get; set; }

        [InverseProperty(nameof(SelfMetersReadEntity.Customer))]
        public virtual ICollection<SelfMetersReadEntity>? SelfMetersRead { get; set; }
    }
}
