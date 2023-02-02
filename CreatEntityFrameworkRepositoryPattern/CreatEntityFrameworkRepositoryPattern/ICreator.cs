using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatEntityFrameworkRepositoryPattern
{
    public class ICreator
    {
        public string FilePath { get; private set; }
        public bool _IsNetCore { get; private set; }
        public ICreator(bool IsNetCore)
        {
                _IsNetCore = IsNetCore;
                string[] subdirs = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory);
                List<string> lstFolderName = new List<string>();
                foreach (var item in subdirs)
                {
                    var dir = new DirectoryInfo(item);
                    var dirName = dir.Name;
                    lstFolderName.Add(dirName);
                }
                //Output資料夾 不存在就建立一個
                if (!lstFolderName.Contains("Output")) Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output"));
                //Output資料夾 存在就砍掉重建
                else
                {
                    Directory.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output"), true);
                    Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output"));
                }
            
            FilePath   = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");
       
        }
       

        public void CreatIRepository(string strNamespace)
        {
            string Temp =
$@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace {strNamespace}" +
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
            string Temp2 =
$@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace {strNamespace}" +
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
            string reuslt = _IsNetCore == true ? Temp2 : Temp;
            File.WriteAllText(Path.Combine(FilePath, $"IRepository.cs"), reuslt);
            string readText = File.ReadAllText(Path.Combine(FilePath, $"IRepository.cs"));
        }
        public void CreatIRepository(string strNamespace, string strClassName)
        {
            string Temp =
$@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace {strNamespace}" +
@"
{
" + $@"public interface I{strClassName}Repository : IRepository<{strClassName}>
" + @"
    {

    }
}
";
            File.WriteAllText(Path.Combine(FilePath, $"I{strClassName}Repository.cs"), Temp);
            string readText = File.ReadAllText(Path.Combine(FilePath, $"I{strClassName}Repository.cs"));
        }
        public void CreatRepository(string strNamespace)
        {
            string Temp =
$@"using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace {strNamespace}" +
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
            string Temp2 =
$@"using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace {strNamespace}" +
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
            string reuslt = _IsNetCore == true ? Temp2 : Temp;
            File.WriteAllText(Path.Combine(FilePath, $"Repository.cs"), reuslt);
            string readText = File.ReadAllText(Path.Combine(FilePath, $"Repository.cs"));
        }
        public void CreatRepository(string strNamespace, string ClassName,string ModelName)
        {
            string Temp =
$@"using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace {strNamespace}" + @"
{" + $@"
    public class {ClassName}Repository : Repository<{ClassName}>, I{ClassName}Repository " + @"
    {" + $@"
        public {ClassName}Repository("+$"{ModelName}"+" context) : base(context)" + @"
        {
        }
        "+$"public {ModelName} {ModelName}"+ @"
        {
            get { return Context as "+ $"{ModelName}" + @"; }
        }
    }
}
";
            File.WriteAllText(Path.Combine(FilePath, $"{ClassName}Repository.cs"), Temp);
            string readText = File.ReadAllText(Path.Combine(FilePath, $"{ClassName}Repository.cs"));
        }

        public void CreatIUnitOfWork(string strNamespace, string ClassName)
        {
            string Temp =
$@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace {strNamespace}" + @"
{
    public interface IUnitOfWork : IDisposable
    {
        "+ $"I{ClassName}Repository {ClassName}"+@" { get; }
        int Complete();
    }
}
";
            File.WriteAllText(Path.Combine(FilePath, $"IUnitOfWork.cs"), Temp);
            string readText = File.ReadAllText(Path.Combine(FilePath, $"IUnitOfWork.cs"));
        }
        public void CreatUnitOfWork(string strNamespace, string ClassName,string ModelName)
        {
            string Temp =
$@"using System.Collections.Generic;
using System.Linq;

namespace {strNamespace}
"+@"{
    public class UnitOfWork : IUnitOfWork
    {
        "+$"private readonly {ModelName} _context;"+@"
        "+$"public I{ClassName}Repository {ClassName} "+@"{ get; private set; }

        "+$"public UnitOfWork({ModelName} context)"+@"
        {
            _context = context;
             "+$"{ClassName} = new {ClassName}Repository(_context);"+@"
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
            File.WriteAllText(Path.Combine(FilePath, $"UnitOfWork.cs"), Temp);
            string readText = File.ReadAllText(Path.Combine(FilePath, $"UnitOfWork.cs"));
        }


    }
}
