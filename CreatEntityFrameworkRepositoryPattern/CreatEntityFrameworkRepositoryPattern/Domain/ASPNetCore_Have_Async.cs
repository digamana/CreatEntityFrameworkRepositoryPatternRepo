using CreatEntityFrameworkRepositoryPattern.Domain.Dto;
using CreatEntityFrameworkRepositoryPattern.Domain.IDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatEntityFrameworkRepositoryPattern.Domain
{
    public class ASPNetCore_Have_Async : ICreatFile
    {
        public ASPNetCore_Have_Async(ArguName arguName, WriteFile writeFile) : base(arguName, writeFile)
        {
            this._arguName = arguName;
            this.writeFile = writeFile;
        }

        protected override string Base_IRepository(ArguName arguName)
        {
            string Temp =
$@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace {arguName.strNamespace}" +
@"
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(string id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity,bool>> predicate =null);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null, bool tracked=true);
        Task AddAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task SaveAsync();
    }
}
";
            return Temp;
        }

        protected override string Base_Repository(ArguName arguName)
        {
            string Temp =
$@"
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace {arguName.strNamespace}" +
@"
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class 
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public TEntity Get(string id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            if(predicate!=null)query= query.Where(predicate);   
            var result = await query.ToListAsync();
            return result;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null, bool tracked = true)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            if(tracked!=true)query=query.AsNoTracking();
            if(predicate!=null)query = query.Where(predicate);
            var result = await query.FirstOrDefaultAsync();
            return result;
        }

        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public async Task RemoveAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            await SaveAsync();
        }
    }
}

";
            return Temp;
        }

        protected override string Child_IRepository(ArguName arguName)
        {
           string Temp= $@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace {arguName.strNamespace}" +
    @"
{
" + $@"public interface I{arguName.ClassName}Repository : IRepository<{arguName.ClassName}>
" + @"
    {

    }
}
";
            return Temp;
        }

        protected override string Child_Repository(ArguName arguName)
        {
            string Temp =
$@"
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace {arguName.strNamespace}" + @"
{" + $@"
    public class {arguName.ClassName}Repository : Repository<{arguName.ClassName}>, I{arguName.ClassName}Repository " + @"
    {" + $@"
        public {arguName.ClassName}Repository(" + $"{arguName.ModelName}" + " context) : base(context)" + @"
        {
        }
        " + $"public {arguName.ModelName} {arguName.ModelName}" + @"
        {
            get { return Context as " + $"{arguName.ModelName}" + @"; }
        }
    }
}
";
            return Temp;
        }



        protected override string IUnitOfWork(ArguName arguName)
        {
            string Temp =
$@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace {arguName.strNamespace}" + @"
{
    public interface IUnitOfWork : IDisposable
    {
        " + $"I{arguName.ClassName} Repository  {arguName.ClassName}" + @" { get; }
        int Complete();
    }
}
";
            return Temp;
        }

        protected override string UnitOfWork(ArguName arguName)
        {
            string Temp =
$@"
using System.Collections.Generic;
using System.Linq;

namespace {arguName.strNamespace}
" + @"{
    public class UnitOfWork : IUnitOfWork
    {
        " + $"private readonly {arguName.ModelName} _context;" + @"
        " + $"public I{arguName.ClassName}Repository {arguName.ClassName} " + @"{ get; private set; }

        " + $"public UnitOfWork({arguName.ModelName} context)" + @"
        {
            _context = context;
                " + $"{arguName.ClassName} = new {arguName.ClassName}Repository(_context);" + @"
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
";
            return Temp;
        }
    }
}
