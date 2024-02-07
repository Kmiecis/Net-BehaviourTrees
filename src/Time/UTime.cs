using System;

namespace Common.BehaviourTrees
{
    internal static class UTime
    {
        public static long UtcNow
            => DateTime.UtcNow.Ticks;

        public static long ToTicks(float s)
            => (long)(TimeSpan.TicksPerSecond * s);

        public static float ToSeconds(long t)
            => (float)(t / TimeSpan.TicksPerSecond);
    }
}