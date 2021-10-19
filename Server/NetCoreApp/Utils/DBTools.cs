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
        //[BsonId]
        public ObjectId Id { get; set; }
        public string name { get; set; }
        public int number { get; set; }

        public User()
        {
        }

        public User(string name, int number)
        {
            //this.Id = id;
            this.name = name;
            this.number = number;
        }

        public override string ToString()
        {
            return $"name={name}, number={number}";
        }
    }

    public class DBTools
    {
        public const string connectionString = "mongodb://localhost:27017/?readPreference=primary&directConnection=true&ssl=false"; //TODO: 放到配置中，而不是代码中

        public static uint QueryLogin(string userName, string pwd)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("stickerDB"); //数据库
            var collection = database.GetCollection<User>("Users"); //表

            // 多条件查询
            var filter = Builders<User>.Filter.Eq(x => x.name, userName) & Builders<User>.Filter.Eq(x => x.number, 10);
            var result = Task.Run(async () => await collection.Find(filter).ToListAsync()).Result;
            Debug.Print($"result count={result.Count}");

            if (result.Count > 0) //==1
                return 0;
            else
                return 101;
        }
    }
}