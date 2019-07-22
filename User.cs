using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbConsoleTestV2
{
    public class User
    {
        public ObjectId Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
        public List<Language> Languages { get; set; }
        public Company Company { get; set; }
    }
    public class Language
    {
        public string Name { get; set; }
        public int Level { get; set; }
    }
    public class Company
    {
        public string Name { get; set; }
    }
}
