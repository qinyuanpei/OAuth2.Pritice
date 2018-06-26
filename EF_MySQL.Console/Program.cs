using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_MySQL.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<MyEntity>());
            //DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
            using (MyEntity db = new MyEntity())
            {
                using (var transcation = db.Database.BeginTransaction())
                {
                    for(int i=0;i<10000;i++)
                    {
                        var foo = new Foo { Id = 666, Name = "测试部", Work = "BA" };
                        db.Bars.Add(foo);
                    }

                    db.SaveChanges();
                }
            }
        }
    }
}
