using System;
using System.Threading;

namespace _18.Deadlock
{
    internal static class TimedMonitor
    {
        internal static LockHelp Lock(this object guard, int timeout)
        {
            TimeSpan ts = TimeSpan.FromMilliseconds(timeout);
            bool lockTaken = false;
            try
            {
                Monitor.TryEnter(guard, ts, ref lockTaken);
                if (lockTaken)
                {
                    return new LockHelp(guard);
                }
                else
                {
                    throw new TimeoutException("lock timed out");
                }
            }
            catch
            {
                if (lockTaken)
                {
                    Monitor.Exit(guard);
                }
                throw;
            }
        }
        internal struct LockHelp : IDisposable
        {
            private readonly object _guard;
            public LockHelp(object guard)
            {
                _guard = guard;
            }
            public void Dispose()
            {
                Monitor.Exit(_guard);
            }
        }
    }
}
