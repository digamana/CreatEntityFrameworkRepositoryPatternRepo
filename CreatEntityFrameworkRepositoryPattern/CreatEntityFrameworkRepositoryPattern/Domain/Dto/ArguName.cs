namespace CreatEntityFrameworkRepositoryPattern.Domain.Dto
{
    public class ArguName
    {
        public string strNamespace { get; set; }
        public string ClassName { get; set; }
        public string ModelName { get; set; }
        private ArguName(string strNamespace, string ClassName, string ModelName)
        { 
            this.strNamespace = strNamespace;
            this.ClassName = ClassName;
            this.ModelName = ModelName;
        }
        public static ArguName Creat(string strNamespace, string ClassName, string ModelName)
        {
            return new ArguName(strNamespace, ClassName, ModelName);
        }
    }
}
