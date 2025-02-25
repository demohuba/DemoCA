using JEPCO.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEPCO.Domain.Entities
{
    [Table("lk_customer_meter_relation_types")]

    public class LK_CustomerMeterRelationTypeEntity : BaseLookupEntity
    {
        public virtual ICollection<CustomerMeterEntity>? CustomerMeters { get; set; }
    }
}
