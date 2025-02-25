using JEPCO.Application.Interfaces.Repositories.Base;
using JEPCO.Domain.Entities;

namespace JEPCO.Application.Interfaces.Repositories;

public interface IAuditLogRepo : IRepository<AuditLogEntity>
{
    Task<List<AuditLogEntity>> GetDataPatchAsync(int patchCount);

    Task TruncateAuditLogsTableAsync(bool resetIdentitySequence);
}
