using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend.API
{
    abstract class Api
    {
        public Wrapped Inner { get; protected set; }

        public Api(Wrapped inner)
        {
            Inner = inner;
        }
    }
}
