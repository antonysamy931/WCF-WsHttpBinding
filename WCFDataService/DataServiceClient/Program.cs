using DataServiceClient.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.AntonyEntities entity = new ServiceReference1.AntonyEntities(new Uri(
                "http://localhost:58854/AntonyWcfDataService.svc/"
                ));

            var obj = entity.Table_1.ToList();

            //if (!obj.Any(x => x.Name.Equals("Kling", StringComparison.InvariantCultureIgnoreCase)))
            //{

            //    Table_1 table = new Table_1();
            //    table.Name = "Kling";
            //    entity.AddObject("Table_1", table);
            //    entity.SaveChanges();
            //}

            //var obj1 = entity.Table_1.ToList();

            //var kling = entity.Table_1.Where(x => x.Id == 7).FirstOrDefault();
            //if (kling != null)
            //{
            //    kling.Name = "Kling Mery";
            //    entity.UpdateObject(kling);
            //    entity.SaveChanges();            
            //}

            //var kling = entity.Table_1.Where(x => x.Id == 7).FirstOrDefault();
            //if (kling != null)
            //{
            //    entity.DeleteObject(kling);
            //    entity.SaveChanges();
            //}

            var obj1 = entity.Depts.ToList();

            //entity.AddToDepts(new Dept() { Dept1 = "OP" });

            //DataServiceResponse response = entity.SaveChanges();

            //foreach (ChangeOperationResponse res in response)
            //{
            //    EntityDescriptor discriptor = res.Descriptor as EntityDescriptor;
            //    if (discriptor != null)
            //    {
            //        Dept deptartment = discriptor.Entity as Dept;
            //        if (deptartment != null)
            //        {
            //            Console.WriteLine(deptartment.Dept1 + " is newly added");
            //        }
            //    }
            //}

            var obj2 = entity.ParentTables.Expand("ChildTables").ToList();

            Console.Read();
        }
    }
}
