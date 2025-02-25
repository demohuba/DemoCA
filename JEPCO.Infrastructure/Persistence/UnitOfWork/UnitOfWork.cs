using JEPCO.Application.Interfaces.Repositories;
using JEPCO.Application.Interfaces.UnitOfWork;
using JEPCO.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace JEPCO.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;
        private IDbContextTransaction _transaction;
        private IAuditLogRepo _auditLogRepo;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public IAuditLogRepo AuditLogRepo
        {
            get
            {
                _auditLogRepo ??= new AuditLogRepo(dbContext);
                return _auditLogRepo;
            }
        }


        public int Complete()
        {
            return dbContext.SaveChanges();
        }
        public Task<int> CompleteAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            _transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            return _transaction;
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await DisposeTransactionAsync();
            }
        }
        private async Task DisposeTransactionAsync()
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await DisposeTransactionAsync();
            }
        }
    }
}
