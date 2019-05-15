using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DefaultNamespace
{
    public class GameResult
    {
        public static Dictionary<string, string> GetParameters() {
            return File.ReadAllLines(Directory.GetCurrentDirectory() + "/game.result")
                .Select(l => l.Split(new[] { '=' }))
                .ToDictionary( s => s[0].Trim(), s => s[1].Trim());
        }
    }
}