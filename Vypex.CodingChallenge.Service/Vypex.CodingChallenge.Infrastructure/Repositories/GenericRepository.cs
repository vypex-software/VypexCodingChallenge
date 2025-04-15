using Microsoft.EntityFrameworkCore;
using Vypex.CodingChallenge.Domain.Interfaces;
using Vypex.CodingChallenge.Domain.Models;
using Vypex.CodingChallenge.Infrastructure.Data;

namespace Vypex.CodingChallenge.Infrastructure.Repositories;

public class GenericRepository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;
    private readonly IUserContext _userContext;

    public GenericRepository(ApplicationDbContext context, IUserContext userContext)
    {
        _context = context;
        _dbSet = _context.Set<T>();
        _userContext = userContext;
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        if (entity is AuditableEntity auditable)
        {
            auditable.Createdon = DateTime.UtcNow;
            auditable.CreatedBy = _userContext.UserName ?? "system";
        }
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public void Update(T entity, CancellationToken cancellationToken)
    {
        if (entity is AuditableEntity auditable)
        {
            auditable.ModifiedOn = DateTime.UtcNow;
            auditable.ModifiedBy = _userContext.UserName ?? "system";
        }
        _dbSet.Update(entity);
    }

    public void Delete(T entity, CancellationToken cancellationToken)
    {
        if (entity is AuditableEntity auditable)
        {
            auditable.DeletedOn = DateTime.UtcNow;
            auditable.DeletedBy = _userContext.UserName ?? "system";
            _dbSet.Update(entity);
        }
        else
        {
            _dbSet.Remove(entity);
        }
    }
}
