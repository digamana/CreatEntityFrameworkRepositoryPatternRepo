using CreatEntityFrameworkRepositoryPattern.Domain.Dto;
using CreatEntityFrameworkRepositoryPattern.Domain.IDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatEntityFrameworkRepositoryPattern.Domain
{
    public class WtihDapper : ICreatFile
    {
        public WtihDapper(ArguName arguName, WriteFile writeFile):base(arguName, writeFile) 
        {
            this._arguName = arguName;
            this.writeFile = writeFile;
        }

        protected override string Base_IRepository(ArguName arguName)
        {
            string temp = @"
using System.Data.SqlClient;
using System.Data;

namespace Domain
{
    public interface IUserRepository
    {
        SqlConnection _sqlConnection { get; }

        IDbTransaction _dbTransaction { get; }
        bool Credit(string userId, double amount);
        bool Debit(string userId, double amount);
    }
}
";
            return temp;
        }

        protected override string Base_Repository(ArguName arguName)
        {
            string temp = @"
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Domain
{
    public class UserRepository : IUserRepository
    {
        public SqlConnection _sqlConnection { get; }

        public IDbTransaction _dbTransaction { get; }
    
        public UserRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
           
        }

        public bool Credit(string userId, double amount)
        {
            var sql = ""UPDATE Users SET CurrentBalance = CurrentBalance + @amount WHERE Id = @userId"";
            return _sqlConnection.Execute(sql, new { userId = userId, amount = amount }, transaction: _dbTransaction) > 0;
        }

        public bool Debit(string userId, double amount)
        {
            var sql = ""UPDATE Users SET CurrentBalance = CurrentBalance - @amount WHERE Id = @userId"";
            return _sqlConnection.Execute(sql, new { userId = userId, amount = amount }, transaction: _dbTransaction) > 0;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var sql = ""SELECT * FROM Users"";
            return _sqlConnection.Query<User>(sql);
        }
    }
}
";
            return temp;
        }

        protected override string Child_IRepository(ArguName arguName)
        {
            return string.Empty;
        }

        protected override string Child_Repository(ArguName arguName)
        {
            return string.Empty;
        }

    

        protected override string IUnitOfWork(ArguName arguName)
        {
            string temp = @"
using System.Data;
using System.Data.SqlClient;

namespace Domain
{
    public interface IUnitOfWork
    {
        SqlConnection _sqlConnection { get; }

        IDbTransaction _dbTransaction { get; }

        IUserRepository _IUserRepository { get; }
    }
}
";
            return temp;
        }

        protected override string UnitOfWork(ArguName arguName)
        {
            string temp = @"
using System.Data;
using System.Data.SqlClient;

namespace Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        public SqlConnection _sqlConnection { get; }
        public IDbTransaction _dbTransaction { get; }
        public IUserRepository _IUserRepository { get; private set; }
        public UnitOfWork(SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _IUserRepository=new UserRepository(sqlConnection, dbTransaction);
        }
    }
    public void Exaple()//範例結構
    {
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings[""DemmoDapper""].ConnectionString;
        SqlConnection con = new SqlConnection(cs);
        IDbTransaction dbTransaction = con.BeginTransaction();
        try
        {
            con.Query<User>(""Select aaa as N'Stus' from  bbb"", dbTransaction);
        }
        catch(System.Exception e)
        {
            dbTransaction.Rollback();
        }
        finally
        {
            dbTransaction.Dispose();
            con.Dispose();
        }
    }
}
";
            return temp; 

        }
    }
}
