using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassesLibrary
{
    public interface IStorage
    {
        void Store(string key, string value);
        string Load(string key);
        IEnumerable<string> GetKeys();
        bool IsKeyExist(string key);
    }
}
