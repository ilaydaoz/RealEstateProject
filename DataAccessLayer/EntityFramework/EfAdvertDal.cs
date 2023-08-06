using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using MongoDB.Driver;

namespace DataAccessLayer.EntityFramework
{
    public class EfAdvertDal : IAdvertDal
    {
        private readonly IMongoCollection<Advert> _advert;

        public EfAdvertDal()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("RealEstateDB");
            _advert = database.GetCollection<Advert>("Advert");
        }

        public void Delete(Advert t)
        {
            _advert.DeleteOne(x => x.Id == t.Id);
        }

        public Advert GetByID(string id)
        {
            return _advert.Find(x => x.Id == id).FirstOrDefault();
        }

        public List<Advert> GetList()
        {
            return _advert.Find(x => true).ToList();
        }

        public void Insert(Advert t)
        {
            _advert.InsertOne(t);            
        }

        public void Update(Advert t)
        {
            _advert.ReplaceOne(x => x.Id == t.Id, t);
        }
    }
}
