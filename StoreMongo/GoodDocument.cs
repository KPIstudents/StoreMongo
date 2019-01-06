using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMongo
{
    enum Good_type
    {
        Default = 0,
        Prod = 1,
        Prom = 2,
        Alkogol = 3
    }
    public class GoodDocument
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public int Type { get; set; }
    }

    public class Prod: GoodDocument
    {

    }

    public class Alkogol : Prod
    {

    }

    public class Prom : GoodDocument
    {

    }
}
