using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEPCO.Domain.Entities
{
    [Table("self_meters_reads")]
    public class SelfMetersReadEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty(nameof(CustomerEntity.SelfMetersRead))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual CustomerEntity? Customer { get; set; }

        public string? DeviceId { get; set; }
        public string? MeterNumber { get; set; }
        public long? FirstReadNumber { get; set; }
        public string? FirstReadImage { get; set; }

        public long? SecondReadNumber { get; set; }
        public string? SecondReadImage { get; set; }

        public int SapStatusId { get; set; }

        [ForeignKey(nameof(SapStatusId))]
        [InverseProperty(nameof(LK_SelfMeterReadSapStatusEntity.SelfMetersReads))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public LK_SelfMeterReadSapStatusEntity? SapStatus { get; set; }

        public DateTime? ScanDate { get; set; }
        public Guid? ExportedBy { get; set; }
        public DateTime? ExportedDate { get; set; }
        public bool? FirstMeterReadVerified { get; set; }
        public bool? SecondMeterReadVerified { get; set; }
    }
}
