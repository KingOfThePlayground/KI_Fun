using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend.API
{
    class ApiCollection<T_Api> : IEnumerable<T_Api> where T_Api : Api
    {
        IEnumerable<Wrapped> _inner;

        public ApiCollection(IEnumerable<Wrapped> inner)
        {
            _inner = inner;
        }

        public IEnumerator<T_Api> GetEnumerator()
        {
            return new Enumerator(_inner.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        class Enumerator : IEnumerator<T_Api>
        {
            IEnumerator<Wrapped> _innerEnumerator;

            public Enumerator(IEnumerator<Wrapped> innerEnumerator)
            {
                _innerEnumerator = innerEnumerator;
            }

            public T_Api Current => (T_Api)_innerEnumerator.Current.Api;

            object IEnumerator.Current => _innerEnumerator.Current;

            public void Dispose()
            {
                _innerEnumerator.Dispose();
            }

            public bool MoveNext()
            {
                return _innerEnumerator.MoveNext();
            }

            public void Reset()
            {
                _innerEnumerator.Reset();
            }
        }
    }
}
