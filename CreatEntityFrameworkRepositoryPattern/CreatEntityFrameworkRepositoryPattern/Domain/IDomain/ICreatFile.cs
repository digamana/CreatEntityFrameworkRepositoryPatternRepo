using CreatEntityFrameworkRepositoryPattern.Domain.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatEntityFrameworkRepositoryPattern.Domain.IDomain
{
    public abstract class ICreatFile
    {
        public ArguName _arguName { get;protected set; }
        public WriteFile writeFile { get;protected set; }
        protected abstract string Base_IRepository(ArguName arguName);
        protected abstract string Base_Repository(ArguName arguName);
        protected abstract string Child_Repository(ArguName arguName);
        protected abstract string Child_IRepository(ArguName arguName);

        protected abstract string UnitOfWork(ArguName arguName);
        protected abstract string IUnitOfWork(ArguName arguName);
        protected string strCodeContext = string.Empty;
        public ICreatFile(ArguName arguName, WriteFile writeFile)
        {
            this._arguName = arguName;
            this.writeFile = writeFile;
        }
        public void CreatFile()
        {
            string strBaseIRepository = Base_IRepository(_arguName).Trim('\r', '\n');
            string strBaseRepository = Base_Repository(_arguName).Trim('\r', '\n');
            string strChildIRepository = Child_IRepository(_arguName).Trim('\r', '\n');
            string strChildRepository = Child_Repository(_arguName).Trim('\r', '\n');
            string strUnitOfWork = UnitOfWork(_arguName).Trim('\r', '\n');
            string strIUnitOfWork = IUnitOfWork(_arguName).Trim('\r', '\n');
            if(string.IsNullOrEmpty(strBaseIRepository)==false)writeFile.Run(this.writeFile._FilePath, GetFilePath(FileNameType.IRepository), strBaseIRepository);
            if (string.IsNullOrEmpty(strChildRepository) == false) writeFile.Run(this.writeFile._FilePath, GetFilePath($"{_arguName.ClassName}Repository.cs"), strChildRepository);
            if (string.IsNullOrEmpty(strChildIRepository) == false) writeFile.Run(this.writeFile._FilePath, GetFilePath($"I{_arguName.ClassName}Repository.cs") , strChildIRepository);
            if (string.IsNullOrEmpty(strBaseRepository) == false) writeFile.Run(this.writeFile._FilePath, GetFilePath(FileNameType.Repository) , strBaseRepository);
            if (string.IsNullOrEmpty(strUnitOfWork) == false) writeFile.Run(this.writeFile._FilePath, GetFilePath(FileNameType.UnitOfWork), strUnitOfWork);
            if (string.IsNullOrEmpty(strIUnitOfWork) == false) writeFile.Run(this.writeFile._FilePath, GetFilePath(FileNameType.IUnitOfWork), strIUnitOfWork);
          
        }
        protected string GetFilePath(string strFilePath)
        {
            return Path.Combine(writeFile._FilePath, strFilePath);
        }
    }
}
