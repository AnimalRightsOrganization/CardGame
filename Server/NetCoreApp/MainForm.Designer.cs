using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Driver.Linq;
using NetCoreServer.Utils;

namespace WinFormsApp1
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

    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.titleLabel = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.updateBtn = new System.Windows.Forms.Button();
            this.queryBtn = new System.Windows.Forms.Button();
            this.startServerBtn = new System.Windows.Forms.Button();
            this.shutDownBtn = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(142, 184);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(68, 17);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "MongoDB";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.connectToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 70);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.maxToolStripMenuItem,
            this.windowToolStripMenuItem});
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.showToolStripMenuItem.Text = "显示";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.ShowToolStripMenuItem_Click);
            // 
            // maxToolStripMenuItem
            // 
            this.maxToolStripMenuItem.Name = "maxToolStripMenuItem";
            this.maxToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.maxToolStripMenuItem.Text = "Max";
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.exitToolStripMenuItem.Text = "退出";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.connectToolStripMenuItem.Text = "连接DB";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.ConnectToolStripMenuItem_Click);
            // 
            // insertBtn
            // 
            this.insertBtn.Location = new System.Drawing.Point(142, 225);
            this.insertBtn.Name = "insertBtn";
            this.insertBtn.Size = new System.Drawing.Size(75, 23);
            this.insertBtn.TabIndex = 1;
            this.insertBtn.Text = "插入";
            this.insertBtn.UseVisualStyleBackColor = true;
            this.insertBtn.Click += new System.EventHandler(this.Button1_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(142, 254);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(75, 23);
            this.deleteBtn.TabIndex = 2;
            this.deleteBtn.Text = "删除";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.Button2_Click);
            // 
            // updateBtn
            // 
            this.updateBtn.Location = new System.Drawing.Point(142, 283);
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(75, 23);
            this.updateBtn.TabIndex = 3;
            this.updateBtn.Text = "修改";
            this.updateBtn.UseVisualStyleBackColor = true;
            this.updateBtn.Click += new System.EventHandler(this.Button3_Click);
            // 
            // queryBtn
            // 
            this.queryBtn.Location = new System.Drawing.Point(142, 312);
            this.queryBtn.Name = "queryBtn";
            this.queryBtn.Size = new System.Drawing.Size(75, 23);
            this.queryBtn.TabIndex = 4;
            this.queryBtn.Text = "查询";
            this.queryBtn.UseVisualStyleBackColor = true;
            this.queryBtn.Click += new System.EventHandler(this.Button4_Click);
            // 
            // startServerBtn
            // 
            this.startServerBtn.Location = new System.Drawing.Point(132, 357);
            this.startServerBtn.Name = "startServerBtn";
            this.startServerBtn.Size = new System.Drawing.Size(94, 23);
            this.startServerBtn.TabIndex = 5;
            this.startServerBtn.Text = "启动服务器";
            this.startServerBtn.UseVisualStyleBackColor = true;
            this.startServerBtn.Click += new System.EventHandler(this.StartServerBtn_Click);
            // 
            // shutDownBtn
            // 
            this.shutDownBtn.Location = new System.Drawing.Point(132, 386);
            this.shutDownBtn.Name = "shutDownBtn";
            this.shutDownBtn.Size = new System.Drawing.Size(94, 23);
            this.shutDownBtn.TabIndex = 6;
            this.shutDownBtn.Text = "关闭服务器";
            this.shutDownBtn.UseVisualStyleBackColor = true;
            this.shutDownBtn.Click += new System.EventHandler(this.ShutDownBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 665);
            this.Controls.Add(this.shutDownBtn);
            this.Controls.Add(this.insertBtn);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.updateBtn);
            this.Controls.Add(this.queryBtn);
            this.Controls.Add(this.startServerBtn);
            this.Controls.Add(this.titleLabel);
            this.Name = "MainForm";
            this.Text = "Winform+NetCore3.1+MongoDB";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //string connectionString = "mongodb://localhost";
        //string connectionString = "mongodb+srv://<username>:<password>@<cluster-address>/test?w=majority";
        string connectionString = "mongodb://localhost:27017/?readPreference=primary&directConnection=true&ssl=false";

        private void Button1_Click(object sender, System.EventArgs e)
        {
            //InsertUser();
            DBTools.AddUser("test2", "123456");
        }

        private void Button2_Click(object sender, System.EventArgs e)
        {
            //DeleteUser();
            DBTools.DeleteUser("test1");
        }

        private void Button3_Click(object sender, System.EventArgs e)
        {
            UpdateUser();
        }

        private void Button4_Click(object sender, System.EventArgs e)
        {
            QueryUser();
        }

        private void InsertUser()
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("stickerDB"); //数据库
            var collection = database.GetCollection<BsonDocument>("Users"); //表

            // 方法①：class转 BsonDocument
            User p = new User();
            p.name = "yyy";
            p.number = 101;
            collection.InsertOne(p.ToBsonDocument());

            // 方法②：手动创建 BsonDocument
            //var filter = new BsonDocument
            //{
            //    { "name", "xxx" },
            //    { "number", 100 },
            //};
            //collection.InsertOne(filter);
        }

        private void DeleteUser()
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("stickerDB");

            var collection = database.GetCollection<BsonDocument>("Users");
            var deleteFilter = Builders<BsonDocument>.Filter.Eq("number", 101);
            var result = collection.DeleteMany(deleteFilter);
            Debug.Print($"result={result.DeletedCount}");
        }

        private void UpdateUser()
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("stickerDB"); //数据库
            var collection = database.GetCollection<BsonDocument>("Users"); //表

            //Update Data
            var filter = Builders<BsonDocument>.Filter.Eq("name", "Henry") & Builders<BsonDocument>.Filter.Eq("number", 14);
            var update = Builders<BsonDocument>.Update.Set("number", 7);
            var result = collection.UpdateOne(filter, update);
            Debug.Print($"Update result={result.ModifiedCount}");
        }

        private void QueryUser()
        {
            Debug.Print($"connect to: {connectionString}");

            var client = new MongoClient(connectionString);

            //var search1 = client.ListDatabaseNames().ToList();
            //Debug.Print($"有{search1.Count}个database");
            //for (int i = 0; i < search1.Count; i++)
            //{
            //    Debug.Print($"[{i}]---{search1[i]}");
            //}
            //有4个database
            //[0]---admin
            //[1]---config
            //[2]---local
            //[3]---stickerDB

            //  根据数据库名称实例化数据库
            var database = client.GetDatabase("stickerDB");

            // 根据集合名称获取集合
            var collection = database.GetCollection<BsonDocument>("Users");
            var filter = new BsonDocument();

            // 查询集合中的文档
            var search2 = Task.Run(async () => await collection.Find(filter).ToListAsync()).Result;
            // 循环遍历输出
            search2.ForEach(p =>
            {
                Debug.Print($"姓名：{p["name"]}，球衣号码：{p["number"]}");
            });

            //$lt    <   (less  than)
            //$lte   <=  (less than  or equal to)
            //$gt    >   (greater  than)
            //$gte   >=  (greater  than or equal to)
            //var filter3 = Builders<BsonDocument>.Filter.Eq("number", 10); //等于
            var filter3 = Builders<BsonDocument>.Filter.Gte("number", 10); //大于等于
            var result = collection.Find(filter3);
            Debug.Print($"result={result.CountDocuments()}");
        }

        private void TestQuery()
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("stickerDB"); //数据库
            var collection = database.GetCollection<User>("Users"); //表

            // 表总计数
            //var filter = new BsonDocument();
            //var search2 = Task.Run(async () => await collection.Find(filter).ToListAsync()).Result;
            //Debug.Print($"search2={search2.Count}");

            // 查询表中第一条元素
            //var firstDocument = collection.Find(new BsonDocument()).FirstOrDefault();
            //Debug.Print(firstDocument.ToString()); //打印出指定User

            // 单条件查询
            var filter = Builders<User>.Filter.Eq(x => x.number, 10);
            var result = Task.Run(async () => await collection.Find(filter).ToListAsync()).Result;
            Debug.Print($"result={result.Count}");

            // 多条件查询
            //var filter = Builders<User>.Filter.Eq(x => x.name, "Henry") & Builders<User>.Filter.Eq(x => x.number, 13);
            //var result = Task.Run(async () => await collection.Find(filter).ToListAsync()).Result;
            //Debug.Print($"result={result.Count}");
        }

        private void StartServerBtn_Click(object sender, System.EventArgs e)
        {
            Debug.Print("启动服务器");

            TcpChatServer.TCPChatServer.Run();
        }

        private void ShutDownBtn_Click(object sender, System.EventArgs e)
        {
            Debug.Print("关闭服务器");

            TcpChatServer.TCPChatServer.Stop();
        }

        #endregion

        private Label titleLabel;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem showToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem maxToolStripMenuItem;
        private ToolStripMenuItem windowToolStripMenuItem;
        private ToolStripMenuItem connectToolStripMenuItem;
        private Button insertBtn;
        private Button deleteBtn;
        private Button updateBtn;
        private Button queryBtn;
        private Button startServerBtn;
        private Button shutDownBtn;
    }
}