using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMongo
{
    public class GoodDocument
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public int Type { get; set; }
    }

    public class Prod: GoodDocument
    {
        public DateTime ExpDate { get; set; }
    }

    public class Alkogol : Prod
    {
        public int Alco { get; set; }
    }

    public class Prom : GoodDocument
    {
        public int SizeGood { get; set; } 
    }
}
