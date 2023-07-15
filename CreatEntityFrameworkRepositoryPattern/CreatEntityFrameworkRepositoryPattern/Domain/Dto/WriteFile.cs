using System.Diagnostics;
using System.IO;

namespace CreatEntityFrameworkRepositoryPattern.Domain.Dto
{
    public  class WriteFile
    {
        public string _FilePath { get; set; }
        public string _FileName { get; set; }
        public string _FileContext { get; set; }
        private WriteFile(string FilePath, string FileName, string FileContext) 
        {
            this._FilePath = FilePath;
            this._FileName = FileName;
            this._FileContext = FileContext;
        }
        private WriteFile(string FilePath, string FileContext)
        {
            this._FilePath = FilePath;
            this._FileContext = FileContext;
        }
        public static WriteFile Creat(string FilePath, string FileName, string FileContext)
        {
            return new WriteFile( FilePath, FileName,FileContext);
        }
        public static WriteFile Creat(string FilePath,  string FileContext)
        {
            return new WriteFile(FilePath, FileContext);
        }
        public void SetFileName(string FileName)
        {
            this._FileName = FileName;
        }
        public static void OpenFileFolder(string FileName)
        {
            Process.Start(FileName);
        }
        public void Run()
        {
            File.WriteAllText(Path.Combine(this._FilePath, this._FileName), this._FileContext);
        }
        public void Run(string FilePath, string FileName, string FileContext)
        {
            File.WriteAllText(Path.Combine(FilePath, FileName), FileContext);
        }
    }
}
