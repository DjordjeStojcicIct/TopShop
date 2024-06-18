using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShop.Domain.Entities
{
    public class FileT : Entity
    {
        public string Path { get; set; }
        public int Size { get; set; }
    }
}
