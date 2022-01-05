using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary2;
using Dapper;
using Microsoft.VisualBasic;

namespace ManageUser
{
    public class ManageUser
    {
        private const string Conn = "Data Source=DESKTOP-49GF6V0;Initial Catalog=MziuriMain;Integrated Security=True";

        private static string ToMd5(string input)
        {
            var sb = new StringBuilder();
            using var md5 = System.Security.Cryptography.MD5.Create();
            var inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);
            foreach (var t in hashBytes)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        } 
        
        public async Task<User> GetUserById(string id)
        {
            await using var db = new SqlConnection(Conn);
            await db.OpenAsync();
            const string sql = "SELECT * FROM Users WHERE id = @a";
            var user = await db.QueryAsync<User>(sql, new {a = id});
            var enumerable = user.ToList();
            if (enumerable.Count > 0)
            {
                enumerable.ToList()[0].Password = "hidden";
            }            
            return enumerable.Count == 0 ? new User {Id = -1} : enumerable.ToList()[0];
        }
        public async Task<User> GetUserByMail(string mail)
        {
            await using var db = new SqlConnection(Conn);
            await db.OpenAsync();
            const string sql = "SELECT * FROM Users WHERE mail = @a";
            var user = await db.QueryAsync<User>(sql, new {a = mail});
            var enumerable = user.ToList();
            if (enumerable.Count > 0)
            {
                enumerable.ToList()[0].Password = "hidden";
            }            
            return enumerable.Count == 0 ? new User {Id = -1} : enumerable.ToList()[0];
        }
        public async Task<ErrorClass> InsertUserM(User user)
        {
            user.Id = -1;
            user.Password = ToMd5(user.Password);
            
            if (GetUserByMail(user.Mail).Id != 0) return new ErrorClass {ErrorCode = ErrorList.ERROR_DUPLICATION, Description = "Email already exists"};            
        
            
            await using var db = new SqlConnection(Conn);
            await db.OpenAsync();
            const string sql =
                "INSERT INTO Users(name,password,lastname,phone_number,creation_date,mail,role) VALUES(@name,@password,@lastname,@phone_number,@creation_date,@mail,'s')";
            await db.ExecuteAsync(sql, new {name = user.Name, password = user.Password, 
                lastname = user.Lastname, phone_number = user.PhoneNumber, mail = user.Mail,role = "default", creation_date = DateTime.Now});
            
            return new ErrorClass {ErrorCode = ErrorList.OK};
        }
    }

}