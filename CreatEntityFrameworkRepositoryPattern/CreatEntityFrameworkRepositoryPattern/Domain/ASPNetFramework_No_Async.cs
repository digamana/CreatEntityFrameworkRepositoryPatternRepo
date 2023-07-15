using CreatEntityFrameworkRepositoryPattern.Domain.Dto;
using CreatEntityFrameworkRepositoryPattern.Domain.IDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatEntityFrameworkRepositoryPattern.Domain
{
    public class ASPNetFramework_No_Async : ICreatFile
    {
        public ASPNetFramework_No_Async(ArguName arguName, WriteFile writeFile) : base(arguName, writeFile)
        {
            this._arguName = arguName;
            this.writeFile = writeFile;
        }

        protected override string Base_IRepository(ArguName arguName)
        {
            string Temp =
$@"
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace {arguName.strNamespace}" +
@"
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(string id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        // This method was not in the videos, but I thought it would be useful to add.
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
";
            return Temp;
        }

        protected override string Base_Repository(ArguName arguName)
        {
            string Temp =
$@"using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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
            // Here we are working with a DbContext, not PlutoContext. So we don't have DbSets 
            // such as Courses or Authors, and we need to use the generic Set() method to access them.
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            // Note that here I've repeated Context.Set<TEntity>() in every method and this is causing
            // too much noise. I could get a reference to the DbSet returned from this method in the 
            // constructor and store it in a private field like _entities. This way, the implementation
            // of our methods would be cleaner:
            // 
            // _entities.ToList();
            // _entities.Where();
            // _entities.SingleOrDefault();
            // 
            // I didn't change it because I wanted the code to look like the videos. But feel free to change
            // this on your own.
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
    }
}

";
            return Temp;
        }

        protected override string Child_IRepository(ArguName arguName)
        {
            string Temp = 
$@"namespace {arguName.strNamespace}" +@"
{"
+ $@"   
    public interface I{arguName.ClassName}Repository : IRepository<{arguName.ClassName}"+@">
    {

    }
}
";
            return Temp;
        }

        protected override string Child_Repository(ArguName arguName)
        {
string Temp =
$@"namespace {arguName.strNamespace}" + @"
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

namespace {arguName.strNamespace}" + @"
{
    public interface IUnitOfWork : IDisposable
    {
        " + $"I{arguName.ClassName}Repository  {arguName.ClassName}" + @" { get; }
        int Complete();
    }
}
";
            return Temp;
        }

        protected override string UnitOfWork(ArguName arguName)
        {
            string Temp =
$@"namespace {arguName.strNamespace}
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
