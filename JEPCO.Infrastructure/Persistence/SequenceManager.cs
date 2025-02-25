using Microsoft.EntityFrameworkCore;

namespace JEPCO.Infrastructure.Persistence;

internal class SequenceManager
{
    private readonly ApplicationDbContext _dbContext;

    public SequenceManager(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int?> GetNextSequenceValue(string sequenceName)
    {
        using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = $"SELECT nextval('\"{sequenceName}\"');";
            await _dbContext.Database.OpenConnectionAsync();
            var commandResult = await command.ExecuteScalarAsync();
            await _dbContext.Database.CloseConnectionAsync();

            int? result = (int?)Convert.ToInt32(commandResult);
            return result;
        }
    }
}
