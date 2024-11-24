using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matri.Model
{
    public class Lead
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Whatsapp { get; set; }
        public string Comment { get; set; }
        public bool IsMarriageBureau { get; set; }
        public bool IsCalled { get; set; }
    }
}
