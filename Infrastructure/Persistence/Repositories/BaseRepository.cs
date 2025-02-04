﻿using Application.Common;
using Application.Extensions;
using Application.Interfaces;
using Domain.Primitives;
using ErrorOr;
using Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T, TId> : IBaseRepository<T, TId>
        where T : class
        where TId : IValueObject
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().FindAsync(id, cancellationToken) != null;
        }

        public async Task<(List<T>, int totalCount)> GetAllPagedAsync(PaginationParameters paginationParameters, CancellationToken cancellationToken)
        {
            var totalCount = await _dbContext.Set<T>().CountAsync(cancellationToken);
            var entities = await _dbContext.Set<T>()
                .AsQueryable()
                .Paginate(paginationParameters)
                .ToListAsync(cancellationToken);

            return (entities, totalCount);
        }

        public async Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().FindAsync(id, cancellationToken);
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                Error.Validation("BaseRepository.Update", $"entity {typeof(T)} cannot be null.");
                return;
            }

            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
