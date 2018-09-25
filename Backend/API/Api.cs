using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend.API
{
    abstract class Api
    {
        protected Wrapped _inner;

        public Api(Wrapped inner)
        {
            _inner = inner;
        }
    }
}
