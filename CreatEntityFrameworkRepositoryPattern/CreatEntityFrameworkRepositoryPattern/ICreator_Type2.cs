using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatEntityFrameworkRepositoryPattern
{
    public class ICreator_Type2
    {
        public string FilePath { get; private set; }
        public ICreator_Type2()
        {

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

            FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");

        }
        public void CreatIRepository(string strNamespace)
        {
            string Temp =
@"
using System;
using System.Linq;
using System.Linq.Expressions;

" + $"namespace {strNamespace}" + @"
{
    public interface IRepository<T>
    {
        void Create(T entity);

        T Read(Expression<Func<T, bool>> predicate);

        IQueryable<T> Reads();

        void Update(T entity);

        void Update(T entity, params Expression<Func<T, object>>[] updateProperties);

        void Delete(T entity);

        void SaveChanges();
    }
}
";
            File.WriteAllText(Path.Combine(FilePath, $"IRepository.cs"), Temp);
            string readText = File.ReadAllText(Path.Combine(FilePath, $"IRepository.cs"));
        }

        public void CreatIRepository(string strNamespace, string strClassName)
        {
            string Temp =
@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

" + $"namespace {strNamespace}" + @"
{
    " + $"public interface I{strClassName}Repository" + @"
    {
        " + $"List<{strClassName}> Get{strClassName}Dtos();" + @"
    }
}

";
            File.WriteAllText(Path.Combine(FilePath, $"I{strClassName}Repository.cs"), Temp);
            string readText = File.ReadAllText(Path.Combine(FilePath, $"I{strClassName}Repository.cs"));
        }

        public void CreatRepository(string strNamespace)
        {
            string Temp =
@"using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

" + $"namespace {strNamespace}" + @"
{
 
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
        {
            private DbContext Context { get; set; }

 
            public Repository(DbContext inContext)
            {
                Context = inContext;
            }
            public void Create(TEntity entity)
            {
                Context.Set<TEntity>().Add(entity);
            }

            public TEntity Read(Expression<Func<TEntity, bool>> predicate)
            {
                return Context.Set<TEntity>().Where(predicate).FirstOrDefault();
            }

            public IQueryable<TEntity> Reads()
            {
                return Context.Set<TEntity>().AsQueryable();
            }

            public void Update(TEntity entity)
            {
                Context.Entry<TEntity>(entity).State = EntityState.Modified;
            }

            public void Update(TEntity entity, params Expression<Func<TEntity, object>>[] updateProperties)
            {
                Context.Configuration.ValidateOnSaveEnabled = false;

                Context.Entry<TEntity>(entity).State = EntityState.Unchanged;

                if (updateProperties != null)
                {
                    foreach (var property in updateProperties)
                    {
                        Context.Entry<TEntity>(entity).Property(property).IsModified = true;
                    }
                }
            }

            public void Delete(TEntity entity)
            {
                Context.Entry<TEntity>(entity).State = EntityState.Deleted;
            }

            public void SaveChanges()
            {
                Context.SaveChanges();

                // 因為Update 單一model需要先關掉validation，因此重新打開
                if (Context.Configuration.ValidateOnSaveEnabled == false)
                {
                    Context.Configuration.ValidateOnSaveEnabled = true;
                }
            }
        }
    }
";
            File.WriteAllText(Path.Combine(FilePath, $"Repository.cs"), Temp);
            string readText = File.ReadAllText(Path.Combine(FilePath, $"Repository.cs"));
        }

        public void CreatRepository(string strNamespace, string strClassName, string ModelName)
        {
            string Temp =
@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

" + $"namespace {strNamespace}" + @"
{
    " + $"public class {strClassName}Repository : I{strClassName}Repository" + @"
    {" + $@"
        private readonly {ModelName} _db;
        public {strClassName}Repository()" + @"
        {" + $@"
            _db = new {ModelName}();" + @"
        }" + $@"
        public List<{strClassName}> Get{strClassName}Dtos()" + @"
        {
           " + $"var result= _db.{strClassName}.ToList();" + @"
            return result;
        }
    }
}
";
            File.WriteAllText(Path.Combine(FilePath, $"{strClassName}Repository.cs"), Temp);
            string readText = File.ReadAllText(Path.Combine(FilePath, $"{strClassName}Repository.cs"));
        }

        public void CreatIUnitOfWork(string strNamespace)
        {
            string Temp =
$@"using System;

namespace {strNamespace}" + @"
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();

        /// <summary>
        /// 取得某一個Entity的Repository。
        /// 如果沒有取過，會initialise一個
        /// 如果有就取得之前initialise的那個。
        /// </summary>
        /// <returns>Entity的Repository</returns>
        IRepository<T> Repository<T>() where T : class;
        }
    }
";
            File.WriteAllText(Path.Combine(FilePath, $"IUnitOfWork.cs"), Temp);
            string readText = File.ReadAllText(Path.Combine(FilePath, $"IUnitOfWork.cs"));
        }

        public void CreatUnitOfWork(string strNamespace, string ModelName)
        {
            string Temp =
$@"using System;
using System.Collections;
using System.Data.Entity;

namespace {strNamespace}" + @"
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        private bool _disposed;
        private Hashtable _repositories;

        public UnitOfWork()
        {
            " + $"_context = new {ModelName}();" + @"
        }

        /// <summary>
        /// 設定此Unit of work(UOF)的Context。
        /// </summary>
        public UnitOfWork(DbContext context)
            {
                _context = context;
            }
            public void OUT()
            {
               
            }
            /// <summary>
            /// 儲存所有異動。
            /// </summary>
            public int Save()
            {
                return _context.SaveChanges();
            }

            /// <summary>
            /// 清除此Class的資源。
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

        /// <summary>
        /// 清除此Class的資源。
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        /// <summary>
        /// 取得某一個Entity的Repository。
        /// 如果沒有取過，會initialise一個
        /// 如果有就取得之前initialise的那個。
        /// </summary>
        /// <returns>Entity的Repository</returns>
        public IRepository<T> Repository<T>() where T : class
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                            .MakeGenericType(typeof(T)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)_repositories[type];
        }
    }
}
";
            File.WriteAllText(Path.Combine(FilePath, $"UnitOfWork.cs"), Temp);
            string readText = File.ReadAllText(Path.Combine(FilePath, $"UnitOfWork.cs"));
        }
    }
}
