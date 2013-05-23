using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Containers
{
    public class EventArgsContainer<T> : EventArgs
    {
        public T _data { get; private set; }

        public EventArgsContainer(T data)
        {
            _data = data;
        }
    }
}
