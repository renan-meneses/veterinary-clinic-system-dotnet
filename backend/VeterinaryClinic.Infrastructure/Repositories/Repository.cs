using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Domain.Entities;

namespace VeterinaryClinic.Infrastructure.Repositories;

public class Repository<T> : Domain.Contracts.Repositories.IRepository<T> where T : class
{
    protected readonly Data.ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(Data.ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public virtual async Task<(IReadOnlyList<T> Items, int TotalCount)> GetFilteredAsync<TFilter>(
        TFilter filter, CancellationToken cancellationToken = default) where TFilter : class
    {
        var query = _dbSet.AsNoTracking();
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query.ToListAsync(cancellationToken);

        return (items, totalCount);
    }
}

public class AnimalRepository : Repository<Animal>
{
    public AnimalRepository(Data.ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<(IReadOnlyList<Animal> Items, int TotalCount)> GetFilteredAsync<AnimalFilter>(
        AnimalFilter filter, CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsNoTracking().AsQueryable();

        if (!string.IsNullOrEmpty(filter.SearchTerm))
        {
            query = query.Where(a => a.Name.Contains(filter.SearchTerm));
        }

        if (filter.Species.HasValue)
        {
            query = query.Where(a => a.Species == filter.Species.Value);
        }

        if (filter.Status.HasValue)
        {
            query = query.Where(a => a.Status == filter.Status.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Include(a => a.AnimalTutors)
            .ThenInclude(at => at.Tutor)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<Animal?> GetCompleteHistoryAsync(Guid animalId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(a => a.Vaccines)
            .Include(a => a.Consultations)
            .Include(a => a.Hospitalizations)
            .Include(a => a.MedicalRecords)
            .Include(a => a.PetshopAttendances)
            .Include(a => a.AnimalTutors)
            .ThenInclude(at => at.Tutor)
            .FirstOrDefaultAsync(a => a.Id == animalId, cancellationToken);
    }
}

public class TutorRepository : Repository<Tutor>
{
    public TutorRepository(Data.ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Animal>> GetAnimalsByTutorAsync(Guid tutorId, CancellationToken cancellationToken = default)
    {
        return await _context.AnimalTutors
            .Where(at => at.TutorId == tutorId)
            .Select(at => at.Animal)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}