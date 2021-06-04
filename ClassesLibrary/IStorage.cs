using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesLibrary
{
    public interface IStorage
    {
        void Store(string shard, string key, string value);
        void StoreShard(string key, string shard);
        string Load(string shard, string key);
        string LoadShard(string key);
        bool HasTextDuplicates(string text);
        bool IsKeyExist(string shard, string key);
    }
}
