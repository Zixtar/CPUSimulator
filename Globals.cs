﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUSimulator
{
    public static class Globals
    {
        public static Dictionary<string, int>  labelDictionary = new Dictionary<string, int>();
        public static Int64[] MPM = new Int64[65536];
        public static bool BPO,BVI,BI;
        public static bool[] BE = new bool[2];
        public static Int16[] R = new Int16[16];
        public static Int16 IR,SBUS,DBUS,RBUS,SP,PC,FLAG,T,IVR,ADR,MDR,INTA;

    }
}
