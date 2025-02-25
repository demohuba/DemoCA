using JEPCO.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEPCO.Domain.Entities
{
    [Table("customer_meters")]
    public class CustomerMeterEntity : DeletableBaseEntity, IAuditLoggableEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty(nameof(CustomerEntity.CustomerMeters))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual CustomerEntity? Customer { get; set; }

        public string SapFileNo { get; set; } = string.Empty;

        public int RelationTypeId { get; set; }

        [ForeignKey(nameof(RelationTypeId))]
        [InverseProperty(nameof(LK_CustomerMeterRelationTypeEntity.CustomerMeters))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public LK_CustomerMeterRelationTypeEntity? RelationTypes { get; set; }
    }
}
