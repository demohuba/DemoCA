using System.ComponentModel.DataAnnotations.Schema;

namespace JEPCO.Domain.Common;

public class BaseLookupEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public virtual int Id { get; set; }
    public string NameAR { get; set; }
    public string NameEN { get; set; }
    public int? Order { get; set; }
}
