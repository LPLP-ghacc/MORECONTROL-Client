using System;

namespace MoreControl.Utilts
{
    /*AutoClearRAMAutoClearRAMAutoClearRAMAutoClearRAMAutoClearRAMAutoClearRAMAutoClearRAMAutoClearRAM*/
    class ACRAM
    {
        public static void Update()
        {
            if (CheckTime())
                Clear();
        }

        private static void Clear()
        {
            GC.Collect();
            Log.Msg($"ClearRAM {DateTime.Now.Hour}:{DateTime.Now.Minute}");
        }

        private static bool CheckTime()
        {
            var time = UnixTimeNow();
            if (timestamp > time) return false;
            timestamp = time + 300;
            return true;
        }
        private static long UnixTimeNow()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }

        private static long timestamp = 0;
    }
}
