using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Models
{
    public static class Cache
    {
        private static int userActionCounter = 0;
        private static int uniqueInt = 0;

        private static Dictionary<string, bool[]> Results { get; set; } = new Dictionary<string, bool[]>();
        private static Dictionary<string, int> Depths { get; set; } = new Dictionary<string, int>();

        public static bool[] Get(string uid)
        {
            if (!Results.ContainsKey(uid + "_counter(" + userActionCounter + ")"))
            {
                return null;
            }

            return Results[uid + "_counter(" + userActionCounter + ")"];
        }

        public static void Push(string uid, bool[] result)
        {
            Results.Add(uid + "_counter(" + userActionCounter + ")", result);
        }


        public static int GetDepth(string uid)
        {
            if (!Depths.ContainsKey(uid + "_counter(" + userActionCounter + ")"))
            {
                return int.MinValue;
            }

            return Depths[uid + "_counter(" + userActionCounter + ")"];
        }

        public static void PushDepth(string uid, int depth)
        {
            Depths.Add(uid + "_counter(" + userActionCounter + ")", depth);
        }


        public static int GetUniqueInt()
        {
            return uniqueInt++;
        }

        public static void IncUserActionCounter()
        {
            userActionCounter++;
            Results.Clear();
            Depths.Clear();
        }
    }
}
