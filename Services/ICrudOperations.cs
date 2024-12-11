using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHotel.Services
{
    public interface ICrudOperations
    {
        void Create();
        void Read();
        void Update();
        void Delete();
    }
}
