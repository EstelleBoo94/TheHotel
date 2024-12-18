using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHotel.Services
{
    public interface ICrudOperations<T>
    {
        void Create(T list);
        void Read(T list);
        void Update(T list);
        void Delete(T list);
    }
}
