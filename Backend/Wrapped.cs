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
        private Api _api;
        public Api Api
        {
            get
            {
                return _api;
            }
            set
            {
                _api = value;
                GameApi.AddAccess(this);
            }
        }

        public Country Owner { get; set; }
    }
}
