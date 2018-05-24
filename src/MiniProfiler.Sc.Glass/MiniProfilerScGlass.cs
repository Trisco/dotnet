using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackExchange.Profiling.Sc.Glass
{
    public class MiniProfilerScGlass
    {
        private class Lookup<T> : ConcurrentDictionary<object, T> { /* just for brevity */ }
        private static readonly object _nullKeyPlaceholder = new object();

       
        public static void Initialize()
        {
            //register GlassProfiledDependencyResolver?

        }
    }
}
