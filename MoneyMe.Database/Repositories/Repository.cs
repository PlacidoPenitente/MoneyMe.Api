using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyMe.Domain;
using MoneyMe.Domain.Repositories;
using MoneyMe.Infrastructure.Database;
using MoneyMe.Infrastructure.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMe.Infrastructure.Repositories
{
    public class Repository<T, U> : IRepository<T> where T : class, IAggregate<Guid> where U : Entity
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public Repository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual async Task AddAsync(T t)
        {
            var entity = _mapper.Map<U>(t);
            await _context.Set<U>().AddAsync(entity);
        }

        public virtual async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            var entities = await _context.Set<U>().ToListAsync();
            return entities.Select(_mapper.Map<T>).ToList();
        }

        public virtual async Task<T> GetAsync(Guid id)
        { 
            var entity = await _context.Set<U>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<T>(entity);
        }

        public virtual void Update(T t)
        {
            var entity = _mapper.Map<U>(t);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Remove(T t)
        {
            var entity = _mapper.Map<U>(t);
            _context.Set<U>().Remove(entity);
        }
    }
}