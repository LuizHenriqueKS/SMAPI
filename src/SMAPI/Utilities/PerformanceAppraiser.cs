using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley;

namespace StardewModdingAPI.Utilities
{
    public class PerformanceAppraiser
    {

        public static IMonitor Monitor;
        private static readonly Dictionary<string, DateTime> resetMap = new Dictionary<string, DateTime>();
        private static readonly Dictionary<string, DateTime> startMap = new Dictionary<string, DateTime>();
        private static readonly Dictionary<string, long> maxMap = new Dictionary<string, long>();

        public static void Start(string name)
        {
            bool reset = resetMap.ContainsKey(name) && (DateTime.Now - resetMap[name]).TotalSeconds >= 10;
            if (!resetMap.ContainsKey(name) || reset)
            {
                resetMap[name] = DateTime.Now;
            }
            if (reset)
            {
                maxMap.Remove(name);
            }
            startMap[name] = DateTime.Now;
        }

        public static void Finish(string name)
        {
            if (Game1.player != null)
            {
                DateTime now = DateTime.Now;
                long delay = (long)(now - startMap[name]).TotalMilliseconds;
                if (!maxMap.ContainsKey(name) || delay > maxMap[name])
                {
                    maxMap[name] = delay;
                    Monitor.Log($"[{now.ToString("dd/MM/yyyy HH:mm:ss")}] Delay: ({name}) {delay}", LogLevel.Info);
                }
            }
        }

    }
}
