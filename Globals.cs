﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CPUSimulator
{
    public static class Globals
    {
        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged = delegate { };

        public static Dictionary<string, int> labelDictionary = new Dictionary<string, int>();
        public static Int64[] MPM = new Int64[116];
        public static UInt16[] MEM = new UInt16[65536];
        public static bool BPO, BVI, BI, INTA;
        public static bool[] BE = new bool[2];
        public static Int16[] R = new Int16[16];
        public static Int64 MIR;

        private static short _PC;
        private static short _IR;
        private static short _SBUS;
        private static short _DBUS;
        private static short _RBUS;
        private static short _SP;
        private static short _FLAG;
        private static short _T;
        private static short _IVR;
        private static short _ADR;
        private static short _MDR;
        private static short _MAR;

        public static short C
        {
            get => (short)(_FLAG & (short)MastiFLAG.Carry);
            set{}
        }
        public static short Z
        {
            get => (short)(_FLAG & (short)MastiFLAG.Zero);
            set{}
        }
        public static short S
        {
            get => (short)(_FLAG & (short)MastiFLAG.Sign); 
            set{}
        }
        public static short O
        {
            get => (short)(_FLAG & (short)MastiFLAG.Ovf);
            set{}
        }

        public static short PC
        {
            get => _PC; set
            {
                _PC = value;
                PropertyChanged();
            }
        }

        public static short IR
        {
            get => _IR; set
            {
                _IR = value;
                PropertyChanged();
            }
        }
        public static short SBUS
        {
            get => _SBUS; set
            {
                _SBUS = value;
                PropertyChanged();
            }
        }
        public static short DBUS
        {
            get => _DBUS; set
            {
                _DBUS = value;
                PropertyChanged();
            }
        }
        public static short RBUS
        {
            get => _RBUS; set
            {
                _RBUS = value;
                PropertyChanged();
            }
        }
        public static short SP
        {
            get => _SP; set
            {
                _SP = value;
                PropertyChanged();
            }
        }
        public static short FLAG
        {
            get => _FLAG; set
            {
                _FLAG = value;
                PropertyChanged();
                PropertyChanged(nameof(O));
                PropertyChanged(nameof(Z));
                PropertyChanged(nameof(S));
                PropertyChanged(nameof(C));
            }
        }
        public static short T
        {
            get => _T; set
            {
                _T = value;
                PropertyChanged();
            }
        }
        public static short IVR
        {
            get => _IVR; set
            {
                _IVR = value;
                PropertyChanged();
            }
        }
        public static short ADR
        {
            get => _ADR; set
            {
                _ADR = value;
                PropertyChanged();
            }
        }
        public static short MDR
        {
            get => _MDR; set
            {
                _MDR = value;
                PropertyChanged();
            }
        }
        public static short MAR
        {
            get => _MAR; set
            {
                _MAR = value;
                PropertyChanged();
            }
        }

        private static void PropertyChanged([CallerMemberName] string? propertyName = null)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        enum MastiFLAG
        {
            Carry = 0b0000000000000001,
            Zero = 0b0000000000000010,
            Sign = 0b0000000000000100,
            Ovf = 0b0000000000001000,
        }
    }
}
