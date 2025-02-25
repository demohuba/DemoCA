using JEPCO.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEPCO.Domain.Entities
{
    [Table("lk_self_meter_read_sap_status")]
    public class LK_SelfMeterReadSapStatusEntity : BaseLookupEntity
    {
        public virtual ICollection<SelfMetersReadEntity>? SelfMetersReads { get; set; }
    }
}
