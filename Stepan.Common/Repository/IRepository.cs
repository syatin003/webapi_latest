using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stepan.Common.Repository
{
    public interface IRepository<T> where T : class
    {
        T Create();
        T Add(T item);
        T Update(T item);
        void Remove(long id);
    }
}
