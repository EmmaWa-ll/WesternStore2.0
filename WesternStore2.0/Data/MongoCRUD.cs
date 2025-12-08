using MongoDB.Driver;

namespace WesternStore2._0.Data
{
    public class MongoCRUD
    {

        private readonly IMongoDatabase db;

        public MongoCRUD(string connectionstring, string databaseName)
        {
            try
            {
                var client = new MongoClient(connectionstring);
                db = client.GetDatabase(databaseName);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //Crete
        public async Task Create<T>(string collectionName, T entity)
        {
            try
            {
                var collection = db.GetCollection<T>(collectionName);
                await collection.InsertOneAsync(entity);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error to create: {ex.Message}");
            }
        }

        //Read All
        public async Task<List<T>> ReadAll<T>(string collectionName)
        {
            try
            {
                var collection = db.GetCollection<T>(collectionName);
                return await collection.Find(x => true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading all: {ex.Message}");
                return new List<T>();
            }
        }

        //Read By ID
        public async Task<T> ReadById<T>(string collectionName, string id)
        {
            try
            {
                var collection = db.GetCollection<T>(collectionName);
                var filter = Builders<T>.Filter.Eq("Id", id);
                return await collection.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading by id: {ex.Message}");
                return default;
            }
        }

        //update
        public async Task<T?> Update<T>(string collectionName, string id, T entity)
        {
            try
            {
                var collection = db.GetCollection<T>(collectionName);
                var filter = Builders<T>.Filter.Eq("Id", id);
                var result = await collection.ReplaceOneAsync(filter, entity);
                if (result.MatchedCount == 0)
                {
                    return default;
                }
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating: {ex.Message}");
                return default;
            }
        }

        //Delete
        public async Task<string> Delete<T>(string collectionName, string id)
        {
            try
            {
                var collection = db.GetCollection<T>(collectionName);

                var filter = Builders<T>.Filter.Eq("Id", id);
                var result = await collection.DeleteOneAsync(filter);

                if (result.DeletedCount == 0)
                {
                    return "Nothing was deleted. Item was not found.";
                }
                return "Item was deleted sucessfully...";
            }
            catch (Exception ex)
            {
                return $"Error deleting: {ex.Message}";

            }
        }

    }
}
