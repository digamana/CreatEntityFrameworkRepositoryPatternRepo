﻿using System;
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
        public ICreator()
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
            File.WriteAllText(Path.Combine(FilePath, $"IRepository.cs"), Temp);
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
            string Temp2 = $@"UnitOfWork.Model.Repositories";
            string Temp =
$@"using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            File.WriteAllText(Path.Combine(FilePath, $"Repository.cs"), Temp);
            string readText = File.ReadAllText(Path.Combine(FilePath, $"Repository.cs"));
        }
        public void CreatRepository(string strNamespace, string ClassName)
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
        public {ClassName}Repository(EFmodel context) : base(context)" + @"
        {
        }
        public EFmodel EFmodel
        {
            get { return Context as EFmodel; }
        }
    }
}
";
            File.WriteAllText(Path.Combine(FilePath, $"{ClassName}Repository.cs"), Temp);
            string readText = File.ReadAllText(Path.Combine(FilePath, $"{ClassName}Repository.cs"));
        }

        public void CreatIUnitOfWork(string strNamespace)
        {
            string Temp =
$@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Model.IRepositories;

namespace {strNamespace}" + @"
{
    public interface IUnitOfWork : IDisposable
    {
        //public IDemoSheetRepository DemoSheet { get; private set; }
        int Complete();
    }
}
";
            File.WriteAllText(Path.Combine(FilePath, $"IUnitOfWork.cs"), Temp);
            string readText = File.ReadAllText(Path.Combine(FilePath, $"IUnitOfWork.cs"));
        }
        public void CreatUnitOfWork(string strNamespace)
        {
            string Temp =
@"using System.Collections.Generic;
using System.Linq;
using UnitOfWork.Model.IRepositories;
using UnitOfWork.Model.Repositories;

namespace UnitOfWork.Model
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFmodel _context;
        //public IDemoSheetRepository DemoSheet { get; private set; }

        public UnitOfWork(EFmodel context)
        {
            _context = context;
            // DemoSheet = new DemoSheetRepository(_context);
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