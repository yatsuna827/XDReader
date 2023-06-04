using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XDReader
{
    internal static class Util
    {
        public static string RegaxReplace(this string arg1, string pattern, string replacement)
        {
            return Regex.Replace(arg1, pattern, replacement);
        }

        public static readonly string[] TrainerName = { "レオ", "ユータ", "タツキ" };
        public static readonly string[] TeamName = { "バシャーモ", "エンテイ", "ラグラージ", "ライコウ", "メガニウム", "スイクン", "メタグロス", "ヘラクロス" };

        public static uint TickToFrame(this long tick, double frequency) => (uint)(tick / 10_000_000 * frequency);
    }
}
