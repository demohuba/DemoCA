using JEPCO.Application.Interfaces.Repositories;
using JEPCO.Domain.Entities;
using JEPCO.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JEPCO.Infrastructure.Persistence.Repositories;

public class AuditLogRepo : Repository<AuditLogEntity>, IAuditLogRepo
{
    public AuditLogRepo(ApplicationDbContext context) : base(context) { }


    public async Task<List<AuditLogEntity>> GetDataPatchAsync(int patchCount)
    {
        return await GetAllQuerable()
            .Where(a => !a.IsArchived)
            .OrderBy(a => a.DateTime)
            .Take(patchCount)
            .ToListAsync();
    }

    public async Task TruncateAuditLogsTableAsync(bool resetIdentitySequence)
    {
        var command = $"TRUNCATE TABLE audit_logs";

        if (resetIdentitySequence)
        {
            command += " RESTART IDENTITY";
        }
        command += ";";

        var affectedRows = await context.Database.ExecuteSqlRawAsync(command);
    }
}
