using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coroutines.Abstractions
{
    public interface ICoroutine  : IEnumerator
    {
        IEnumerator Enumerator
        {
            get;
        }

        bool keepWaiting
        {
            get;
        }

        void Done();
    }
}
