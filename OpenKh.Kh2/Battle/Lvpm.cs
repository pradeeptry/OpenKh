﻿using System.IO;

namespace OpenKh.Kh2.Battle
{
    public class Lvpm
    {
        public short HpMultiplier { get; set; } // (Hp * HpMultiplier + 99) / 100
        public short Unknown02 { get; set; }
        public short Unknown04 { get; set; }
        public short Unknown06 { get; set; }
        public short Attack { get; set; }
        public short Unknown0a { get; set; }

        public static BaseBattle<Lvpm> Read(Stream stream) => BaseBattle<Lvpm>.Read(stream);
    }
}
