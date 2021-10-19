using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Driver.Linq;
//using User = WinFormsApp1.User;

namespace NetCoreServer.Utils
{
    public class User
    {
        public User() { }
        public User(string _username, string _pwd)
        {
            this.username = _username;
            this.pwd = _pwd;
        }

        //[BsonId]
        public ObjectId Id { get; set; }
        public string username { get; set; } //主键，登录用户名
        public string pwd { get; set; } //主键
        public string nickname { get; set; }
        public byte gender { get; set; }
        public long phoneNumber { get; set; }
        public DateTime createDate { get; set; }

        public override string ToString()
        {
            return $"username={username}, pwd={pwd}";
        }
    }

    public class DBTools
    {
        public const string connectionString = "mongodb://localhost:27017/?readPreference=primary&directConnection=true&ssl=false"; //TODO: 放到配置中，而不是代码中

        public static uint QueryLogin(string userName, string pwd)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("cardGame"); //数据库
            var collection = database.GetCollection<User>("users"); //表

            // 多条件查询
            var filter = Builders<User>.Filter.Eq(x => x.username, userName) & Builders<User>.Filter.Eq(x => x.pwd, pwd);
            var result = Task.Run(async () => await collection.Find(filter).ToListAsync()).Result;
            Debug.Print($"result count={result.Count}");

            if (result.Count == 1)
                return 0;
            else if (result.Count == 0)
                return 100; //没有注册
            else
                return 101; //已有多个重复注册
        }

        //public static void CreateTable()
        //{
        //    var client = new MongoClient(connectionString);
        //    var database = client.GetDatabase("cardGame"); //数据库
        //    database.CreateCollection("users");
        //}

        public static void AddUser(string _username, string _pwd)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("cardGame"); //数据库
            var collection = database.GetCollection<User>("users"); //表

            // 方法①：class转 BsonDocument
            User p = new User(_username, _pwd);
            p.createDate = DateTime.Now;
            collection.InsertOne(p);

            //Debug.Print("insert ok");
        }

        public static void DeleteUser(string _username)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("cardGame"); //数据库

            var collection = database.GetCollection<User>("users"); //表
            var filter = Builders<User>.Filter.Eq("username", _username);
            var result = collection.DeleteMany(filter);
            Debug.Print($"delete count={result.DeletedCount}");
        }
    }
}