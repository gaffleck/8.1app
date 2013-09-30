using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using DataAccess;
using SQLite;

namespace DAL
{
    public sealed class AssetRepo
    {

        private SQLiteAsyncConnection db;


        public AssetRepo()
        {
            db = new SQLiteAsyncConnection(Path.Combine(Windows.Storage.ApplicationData.Current.TemporaryFolder.Path, "db.sqlite"));
            seed();
        }

        private void seed()
        {
            var res = db.CreateTableAsync<Asset>().Result;
            db.InsertAsync(new Asset
            {
                AssetId = 1,
                Description = "test"
            });
        }

        public object GetAsset(int id)
        {
            var ass =  db.FindAsync<Asset>(x => x.AssetId == id).Result;
            return ass.Serialize(typeof(Asset)); 
        }

        public int Save(string asset)
        {
            var asset2 = asset.Deserialize(typeof (Asset));
            return db.InsertAsync(asset2).Result;
        }


    }
}
