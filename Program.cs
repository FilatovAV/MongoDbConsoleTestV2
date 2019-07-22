using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbConsoleTestV2
{
    class Program
    {
        static readonly string con = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
        static readonly string[] first_names = { "Артем", "Кирилл", "Кирилл", "Андрон", "Андреян", "Адам", "Георгий", "Герман", "Захар", "Зиновий", "Игорь", "Илья", "Иван",
        "Моисей", "Марк", "Леонид", "Казимир", "Рудольф", "Семен"};
        static readonly string[] last_names = { "Абаимов", "Абалдуев", "Абрамкин", "Абрашкин", "Бабарыкин", "Бабаченко", "Бабенко", "Бабунин", "Вавилин", "Ваганьков", "Вакулин", "Валерьянов", "Гаврилин",
        "Глуханьков", "Давидович", "Дедюхин", "Добрынский", "Жаденов", "Сарычев"};
        static readonly string[] patronymics = { "Станиславович", "Аркадиевич", "Денисович", "Кириллович", "Артемович", "Семенович", "Викторович", "Андронович", "Игоревич", "Ильич", "Петрович", "Захарович", "Гаврилович",
        "Александрович", "Сергеевич", "Дмитриевич", "Алексеевич", "Григорьевич", "Игоревич"};
        static readonly string[] languages = { "Русский", "Английский", "Немецкий", "Французский", "Испанский", "Японский" };
        static readonly string[] companies = { "ООО \"Газпром\"", "ООО \"Ленаэропроект\"", "ЗАО \"Ленгипротранс\"", "ООО \"Морион\"", "ООО \"Кобальт\"", "ЗАО \"ПЭК\"" };
        static List<BsonDocument> Users = new List<BsonDocument>();

        static void Main(string[] args)
        {

            MongoClient client = new MongoClient(con);

            var database = client.GetDatabase("users_new_test_db");
            var col = database.GetCollection<BsonDocument>("users");

            //--------------------------------------------------------------------------------------------------------------
            //Пример, вставка одного документа в коллекцию
            //User user = new User { Age = 21, FirstName = "Kirill", LastName = "Toshakov", Patronymic = "Vladimirovich" };
            //BsonDocument doc = user.ToBsonDocument<User>();
            //col.InsertOne(doc);
            //--------------------------------------------------------------------------------------------------------------

            InitialData();
            Save_new_doc(col).GetAwaiter().GetResult();

            FindDocs(col).GetAwaiter().GetResult();



            Console.ReadLine();


            //SaveDocs().GetAwaiter().GetResult();

            //Console.ReadLine();
        }

        private static async Task Save_new_doc(IMongoCollection<BsonDocument> col)
        {
            await col.DeleteManyAsync(new BsonDocument());
            await col.InsertManyAsync(Users);
        }

        private static async Task FindDocs(IMongoCollection<BsonDocument> collection)
        {

            //Eq: выбирает только те документы, у которых значение определенного свойсва равно некоторому значению.Например, Builders<Person>.Filter.Eq(Name", "Tom")
            //Gt: выбирает только те документы, у которых значение определенного свойства больше некоторого значения.Например, Builders<Person>.Filter.Gt("Age", 25)
            //Gte: выбирает только те документы, у которых значение определенного свойства больше или равно некоторому значению.Например, Builders<Person>.Filter.Gte("Name", "T")
            //- выбирает все документы, у которых значение Name начинается с буквы T
            //Lt: выбирает только те документы, у которых значение определенного свойства меньше некоторого значения Builders<Person>.Filter.Lt("Age", 25)
            //Lte: выбирает только те документы, у которых значение определенного свойства меньше или равно некоторому значению
            //Ne: выбирает только те документы, у которых значение определенного свойства не равно некоторому значению Builders<Person>.Filter.Ne("Age", 23)
            //In: получает все документы, у которых значение свойства может принимать одно из указанных значений.Например, найдем все документы, у которых свойство Age равно либо 1977, либо 1989, либо 1981: Builders<Person>.Filter.In("Age", new List<BsonInt32>() { 23, 25, 27 });
            //Nin: противоположность оператору In - выбирает все документы, у которых значение свойства не принимает одно из указанных значений




            //string connectionString = "mongodb://localhost";
            //var client = new MongoClient(connectionString);
            //var database = client.GetDatabase("test");
            //var collection = database.GetCollection<BsonDocument>("people");
            //var filter = new BsonDocument();




            //BsonDocument filter = new BsonDocument
            //{
            //    {"FirstName","Kirill"}
            //};
            //----------------------------------------------------------------------
            // оператор $gt выбрать больше чем значение
            //var filter = new BsonDocument("Age", new BsonDocument("$gt", 10));
            //----------------------------------------------------------------------
            //Выбора документов, у которых поле "Age" имеет значение от 15 и выше, либо поле "FirstName" имеет значение "Tom"
            //var filter = new BsonDocument("$or", new BsonArray{
            //    new BsonDocument("Age",new BsonDocument("$gte", 15)),
            //    new BsonDocument("FirstName", "Artem")
            //});
            //----------------------------------------------------------------------
            //Получим документы, в которых одновременно и FirstName="Artem", и значение поля Age больше 10:
            //var filter = new BsonDocument("$and", new BsonArray{
            //    new BsonDocument("Age",new BsonDocument("$gt", 10)),
            //    new BsonDocument("FirstName", "Артем")
            //});
            //----------------------------------------------------------------------


            //FilterDefinitionBuilder

            //Выбрать все документы где Age > 98
            //var filter = Builders<BsonDocument>.Filter.AnyGt("Age", 98);
            //----------------------------------------------------------------------

            //----------------------------------------------------------------------
            //----------------------------------------------------------------------
            //Используем комбинацию значений 2 фильтра
            //var filter1 = Builders<BsonDocument>.Filter.AnyGt("Age", 98);
            //var filter2 = Builders<BsonDocument>.Filter.AnyLt("Age", 2);
            //var filterOr = Builders<BsonDocument>.Filter.Or(new List<FilterDefinition<BsonDocument>> { filter1, filter2 });

            //using (var cursor = await collection.FindAsync(filterOr))
            //{
            //    while (await cursor.MoveNextAsync())
            //    {
            //        var people = cursor.Current;
            //        foreach (var doc in people)
            //        {
            //            Console.WriteLine(doc);
            //        }
            //    }
            //}
            //----------------------------------------------------------------------
            //----------------------------------------------------------------------


            //Условие определяется с помощью лямбда-выражения
            //Builders<Person>.Filter.Where(e => e.Name == "Tom");
            //----------------------------------------------------------------------
            //----------------------------------------------------------------------

            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("FirstName", "Артем") & builder.Gt("Age", 1) /*& builder.AnyEq("Language", "Русский")*/;





            //var result2 = await collection<User>.Find<User>(new BsonDocument()).SortBy(e => e.Age);


            //Оператор Like в Монго
            //db.users.find({name: /a/}) -- like '%a%'
            //db.users.find({name: /^pa/}) -- like 'pa%' 



            //, Languge = {doc?.Languages[0]
            using (var cursor = await collection.FindAsync<User>(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var people = cursor.Current;
                    foreach (var doc in people)
                    {
                        Console.WriteLine($"FirstName = {doc.FirstName};\nLanguage = {((doc.Languages.Count == 0) ? String.Empty : doc.Languages[0].Name) }.");
                    }
                }
            }
        }

        private static void InitialData()
        {
            int age;

            Random r = new Random();
            for (int i = 0; i < 1000; i++)
            {
                age = r.Next(1, 100);

                List<Language> languages_lst = new List<Language>();
                for (int ii = 0; ii < r.Next(1, 4); ii++)
                {
                    languages_lst.Add( new Language { Name = languages[r.Next(0, languages.Length)], Level = (ii == 0 && age > 18) ? 100 : r.Next(0, 101) });
                }

                Users.Add(new User
                {
                    Age = age,
                    FirstName = first_names[r.Next(0, first_names.Length)],
                    LastName = last_names[r.Next(0, last_names.Length)],
                    Patronymic = patronymics[r.Next(0, patronymics.Length)],
                    Languages = languages_lst,
                    Company = new Company { Name = companies[r.Next(0, companies.Length)] }
                }.ToBsonDocument());
            }
        }

        private static async Task SaveDocs()
        {
            string connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("test_mgdb");
            var collection = database.GetCollection<BsonDocument>("people");
            BsonDocument person1 = new BsonDocument
            {
                {"Name", "Bill"},
                {"Age", 32},
                {"Languages", new BsonArray{"english", "german"}}
            };
            BsonDocument person2 = new BsonDocument
            {
                {"Name", "Steve"},
                {"Age", 31},
                {"Languages", new BsonArray{"english", "french"}}
            };
            await collection.InsertManyAsync(new[] { person1, person2 });
        }
    }
}
