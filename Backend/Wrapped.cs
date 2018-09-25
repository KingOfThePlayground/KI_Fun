using KI_Fun.Backend.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend
{
    abstract class Wrapped
    {
        public Api Api { get; protected set; }
    }
}
