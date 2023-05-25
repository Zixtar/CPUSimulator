using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CPUSimulator
{
    public class NotifyingInt16 : INotifyPropertyChanged
    {
        private short _value;
        public event PropertyChangedEventHandler PropertyChanged;

        public NotifyingInt16()
        {
            this._value = 0;
        }

        public short Value
        {
            get
            {
                if (!Globals.MemAction)
                {
                    if (Globals.SBUSAction)
                    {
                        Globals.SBUSRGused = true;
                    }
                    else if (Globals.DBUSAction)
                    {
                        Globals.DBUSRGused = true;
                    }

                    Globals.RGused = true;
                }
               
                return _value;
            }
            set
            {
                Globals.RGused = true;
                Globals.RBUSRGused = true;
                _value = value;
                NotifyPropertyChanged();
            }
        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
    public static class Globals
    {
        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged = delegate { };
        public static Dictionary<string, int> labelDictionary = new Dictionary<string, int>();
        public static Int64[] MPM = new Int64[116];
        public static UInt16[] MEM = new UInt16[65536];
        public static bool BPO, BVI, BI, INTA;
        public static bool[] BE = new bool[2];
        private static BindingList<NotifyingInt16> r = new BindingList<NotifyingInt16>() { new NotifyingInt16(), new NotifyingInt16(), new NotifyingInt16(), new NotifyingInt16(), new NotifyingInt16(), new NotifyingInt16(), new NotifyingInt16(), new NotifyingInt16(), new NotifyingInt16(), new NotifyingInt16(), new NotifyingInt16(), new NotifyingInt16(), new NotifyingInt16(), new NotifyingInt16(), new NotifyingInt16(), new NotifyingInt16() };
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
        private static int _stare;
        private static bool _programLoaded = false;

        public static bool OthAction = false;
        public static bool SBUSAction = false;
        public static bool DBUSAction = false;
        public static bool MemAction = false;

        private static bool _RBUSused = false;
        private static bool _ALURBUSused = false;
        private static bool _DBUSused = false;
        private static bool _SBUSused = false;
        private static bool _DBUSALUused = false;
        private static bool _SBUSALUused = false;
        private static bool _SPused = false;
        private static bool _FLAGused = false;
        private static bool _RBUSFLAGused = false;
        private static bool _Tused = false;
        private static bool _IVRused = false;
        private static bool _ADRused = false;
        private static bool _MDRused = false;
        private static bool _MARused = false;
        private static bool _PCused = false;
        private static bool _RBUSRGused = false;
        private static bool _RGused = false;
        private static bool _RBUSSPused = false;
        private static bool _RBUSTused = false;
        private static bool _RBUSADRused = false;
        private static bool _RBUSMDRused = false;
        private static bool _RBUSMARused = false;
        private static bool _RBUSPCused = false;
        private static bool _RBUSIVRused = false;

        private static bool _SBUSFLAGused = false;
        private static bool _SBUSRGused = false;
        private static bool _SBUSSPused = false;
        private static bool _SBUSTused = false;
        private static bool _SBUSADRused = false;
        private static bool _SBUSMDRused = false;
        private static bool _SBUSMARused = false;
        private static bool _SBUSPCused = false;
        private static bool _SBUSIVRused = false;

        private static bool _DBUSFLAGused = false;
        private static bool _DBUSRGused = false;
        private static bool _DBUSSPused = false;
        private static bool _DBUSTused = false;
        private static bool _DBUSADRused = false;
        private static bool _DBUSMDRused = false;
        private static bool _DBUSMARused = false;
        private static bool _DBUSPCused = false;
        private static bool _DBUSIVRused = false;

        public static bool DBUSIVRused
        {
            get => _DBUSIVRused;
            set
            {
                _DBUSIVRused = value;
                PropertyChanged();
            }
        }
        public static bool DBUSPCused
        {
            get => _DBUSPCused;
            set
            {
                _DBUSPCused = value;
                PropertyChanged();
            }
        }
        public static bool DBUSMARused
        {
            get => _DBUSMARused;
            set
            {
                _DBUSMARused = value;
                PropertyChanged();
            }
        }
        public static bool DBUSMDRused
        {
            get => _DBUSMDRused;
            set
            {
                _DBUSMDRused = value;
                PropertyChanged();
            }
        }
        public static bool DBUSADRused
        {
            get => _DBUSADRused;
            set
            {
                _DBUSADRused = value;
                PropertyChanged();
            }
        }
        public static bool DBUSTused
        {
            get => _DBUSTused;
            set
            {
                _DBUSTused = value;
                PropertyChanged();
            }
        }

        public static bool DBUSSPused
        {
            get => _DBUSSPused;
            set
            {
                _DBUSSPused = value;
                PropertyChanged();
            }
        }
        public static bool DBUSRGused
        {
            get => _DBUSRGused;
            set
            {
                _DBUSRGused = value;
                PropertyChanged();
            }
        }
        public static bool DBUSFLAGused
        {
            get => _DBUSFLAGused;
            set
            {
                _DBUSFLAGused = value;
                PropertyChanged();
            }
        }

        public static bool SBUSFLAGused
        {
            get => _SBUSFLAGused;
            set
            {
                _SBUSFLAGused = value;
                PropertyChanged();
            }
        }
        public static bool SBUSIVRused
        {
            get => _SBUSIVRused;
            set
            {
                _SBUSIVRused = value;
                PropertyChanged();
            }
        }
        public static bool SBUSPCused
        {
            get => _SBUSPCused;
            set
            {
                _SBUSPCused = value;
                PropertyChanged();
            }
        }

        public static bool SBUSMARused
        {
            get => _SBUSMARused;
            set
            {
                _SBUSMARused = value;
                PropertyChanged();
            }
        }
        public static bool SBUSMDRused
        {
            get => _SBUSMDRused;
            set
            {
                _SBUSMDRused = value;
                PropertyChanged();
            }
        }
        public static bool SBUSADRused
        {
            get => _SBUSADRused;
            set
            {
                _SBUSADRused = value;
                PropertyChanged();
            }
        }


        public static bool SBUSTused
        {
            get => _SBUSTused;
            set
            {
                _SBUSTused = value;
                PropertyChanged();
            }
        }
        public static bool SBUSRGused
        {
            get => _SBUSRGused;
            set
            {
                _SBUSRGused = value;
                PropertyChanged();
            }
        }
        public static bool SBUSSPused
        {
            get => _SBUSSPused;
            set
            {
                _SBUSSPused = value;
                PropertyChanged();
            }
        }


        public static bool RBUSIVRused
        {
            get => _RBUSIVRused;
            set
            {
                _RBUSIVRused = value;
                PropertyChanged();
            }
        }

        public static bool RBUSPCused
        {
            get => _RBUSPCused;
            set
            {
                _RBUSPCused = value;
                PropertyChanged();
            }
        }
        public static bool RBUSTused
        {
            get => _RBUSTused;
            set
            {
                _RBUSTused = value;
                PropertyChanged();
            }
        }
        public static bool RBUSADRused
        {
            get => _RBUSADRused;
            set
            {
                _RBUSADRused = value;
                PropertyChanged();
            }
        }
        public static bool RBUSMDRused
        {
            get => _RBUSMDRused;
            set
            {
                _RBUSMDRused = value;
                PropertyChanged();
            }
        }

        public static bool RBUSMARused
        {
            get => _RBUSMARused;
            set
            {
                _RBUSMARused = value;
                PropertyChanged();
            }
        }

        public static bool RBUSSPused
        {
            get => _RBUSSPused;
            set
            {
                _RBUSSPused = value;
                PropertyChanged();
            }
        }

        public static bool RGused
        {
            get => _RGused;
            set
            {
                _RGused = value;
                PropertyChanged();
            }
        }

        public static bool RBUSRGused
        {
            get => _RBUSRGused;
            set
            {
                _RBUSRGused = value;
                PropertyChanged();
            }
        }
        public static bool RBUSFLAGused
        {
            get => _RBUSFLAGused;
            set
            {
                _RBUSFLAGused = value;
                PropertyChanged();
            }
        }

        public static bool PCused
        {
            get => _PCused;
            set
            {
                if (!OthAction)
                {
                    _PCused = value;
                    PropertyChanged();
                }
            }
        }

        public static bool MARused
        {
            get => _MARused;
            set
            {
                _MARused = value;
                PropertyChanged();
            }
        }

        public static bool IVRused
        {
            get => _IVRused;
            set
            {
                _IVRused = value;
                PropertyChanged();
            }
        }
        public static bool ADRused
        {
            get => _ADRused;
            set
            {
                _ADRused = value;
                PropertyChanged();
            }
        }
        public static bool MDRused
        {
            get => _MDRused;
            set
            {
                _MDRused = value;
                PropertyChanged();
            }
        }

        public static bool SPused
        {
            get => _SPused;
            set
            {
                if (!OthAction)
                {
                    _SPused = value;
                    PropertyChanged();
                }
            }
        }
        public static bool FLAGused
        {
            get => _FLAGused;
            set
            {
                if (!OthAction)
                {
                    _FLAGused = value;
                }

                PropertyChanged();
            }
        }
        public static bool Tused
        {
            get => _Tused;
            set
            {
                _Tused = value;
                PropertyChanged();
            }
        }


        public static bool DBUSused
        {
            get => _DBUSused;
            set
            {
                _DBUSused = value;
                PropertyChanged();
            }
        }
        public static bool SBUSused
        {
            get => _SBUSused;
            set
            {
                _SBUSused = value;
                PropertyChanged();
            }
        }
        public static bool DBUSALUused
        {
            get => _DBUSALUused;
            set
            {
                _DBUSALUused = value;
                PropertyChanged();
            }
        }
        public static bool SBUSALUused
        {
            get => _SBUSALUused;
            set
            {
                _SBUSALUused = value;
                PropertyChanged();
            }
        }

        public static bool RBUSused
        {
            get => _RBUSused;
            set
            {
                _RBUSused = value;
                PropertyChanged();
            }
        }

        public static bool ALURBUSused
        {
            get => _ALURBUSused;
            set
            {
                _ALURBUSused = value;
                PropertyChanged();
            }
        }


        public static bool ProgramLoaded
        {
            get => _programLoaded;
            set
            {
                _programLoaded = value;
                PropertyChanged();
            }
        }

        private static bool _programAssembled = false;

        public static bool ProgramAssembled
        {
            get => _programAssembled;
            set
            {
                _programAssembled = value;
                PropertyChanged();
            }
        }

        public static int Stare
        {
            get => _stare;
            set
            {
                _stare = value;
                PropertyChanged();
            }
        }

        public static short C
        {
            get => (short)(_FLAG & (short)MastiFLAG.Carry);
            set { }
        }
        public static short Z
        {
            get => (short)(_FLAG & (short)MastiFLAG.Zero);
            set { }
        }
        public static short S
        {
            get => (short)(_FLAG & (short)MastiFLAG.Sign);
            set { }
        }
        public static short O
        {
            get => (short)(_FLAG & (short)MastiFLAG.Ovf);
            set { }
        }

        public static short PC
        {
            get
            {
                if (!MemAction)
                {
                    if (SBUSAction)
                    {
                        SBUSPCused = true;
                    }
                    else if (DBUSAction)
                    {
                        DBUSPCused = true;
                    }

                    PCused = true;
                }

                return _PC;
            }
            set
            {
                if (!OthAction)
                {
                    RBUSPCused = true;
                }

                PCused = true;
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
            get
            {
                SBUSALUused = true;
                SBUSused = true;
                return _SBUS;
            }
            set
            {
                SBUSused = true;
                _SBUS = value;
                PropertyChanged();
            }
        }

        public static short DBUS
        {
            get
            {
                DBUSALUused = true;
                DBUSused = true;
                return _DBUS;
            }
            set
            {
                DBUSused = true;
                _DBUS = value;
                PropertyChanged();
            }
        }

        public static short RBUS
        {
            get
            {
                RBUSused = true;
                return _RBUS;
            }
            set
            {
                ALURBUSused = true;
                RBUSused = true;
                _RBUS = value;
                PropertyChanged();
            }
        }

        public static short SP
        {
            get
            {
                if (!MemAction)
                {
                    if (SBUSAction)
                    {
                        SBUSSPused = true;
                    }
                    else if (DBUSAction)
                    {
                        DBUSSPused = true;
                    }

                    SPused = true;
                }

                return _SP;
            }
            set
            {
                if (!OthAction)
                {
                    RBUSSPused = true;
                }
                SPused = true;
                _SP = value;
                PropertyChanged();
            }
        }

        public static short FLAG
        {
            get
            {
                if (!MemAction)
                {
                    if (SBUSAction)
                    {
                        SBUSFLAGused = true;
                    }
                    else if (DBUSAction)
                    {
                        DBUSFLAGused = true;
                    }

                    FLAGused = true;
                }

                return _FLAG;
            }
            set
            {
                if (!OthAction)
                {
                    RBUSFLAGused = true;
                }

                FLAGused = true;
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
            get
            {
                if (!MemAction)
                {
                    if (SBUSAction)
                    {
                        SBUSTused = true;
                    }
                    else if (DBUSAction)
                    {
                        DBUSTused = true;
                    }
                }

                Tused = true;
                return _T;
            }
            set
            {
                RBUSTused = true;
                Tused = true;
                _T = value;
                PropertyChanged();
            }
        }

        public static short IVR
        {
            get
            {
                if (!MemAction)
                {
                    if (SBUSAction)
                    {
                        SBUSIVRused = true;
                    }
                    else if (DBUSAction)
                    {
                        DBUSIVRused = true;
                    }

                    IVRused = true;
                }

                return _IVR;
            }
            set
            {
                RBUSIVRused = true;
                IVRused = true;
                _IVR = value;
                PropertyChanged();
            }
        }

        public static short ADR
        {
            get
            {
                if (!MemAction)
                {
                    if (SBUSAction)
                    {
                        SBUSADRused = true;
                    }
                    else if (DBUSAction)
                    {
                        DBUSADRused = true;
                    }

                    ADRused = true;
                }

                return _ADR;
            }
            set
            {
                RBUSADRused = true;
                ADRused = true;
                _ADR = value;
                PropertyChanged();
            }
        }

        public static short MDR
        {
            get
            {
                if (!MemAction)
                {
                    if (SBUSAction)
                    {
                        SBUSMDRused = true;
                    }
                    else if (DBUSAction)
                    {
                        DBUSMDRused = true;
                    }

                    MDRused = true;
                }

                return _MDR;
            }
            set
            {
                RBUSMDRused = true;
                MDRused = true;
                _MDR = value;
                PropertyChanged();
            }
        }

        public static short MAR
        {
            get
            {
                if (!MemAction)
                {
                    if (SBUSAction)
                    {
                        SBUSMARused = true;
                    }
                    else if (DBUSAction)
                    {
                        DBUSMARused = true;
                    }

                    MARused = true;
                }

                return _MAR;
            }
            set
            {
                RBUSMARused = true;
                MARused = true;
                _MAR = value;
                PropertyChanged();
            }
        }

        public static BindingList<NotifyingInt16> R { get => r; set => r = value; }

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

        public static void ResetColors()
        {
            Globals.RBUSIVRused = false;
            Globals.RBUSPCused = false;
            Globals.RBUSused = false;
            Globals.ALURBUSused = false;
            Globals.DBUSused = false;
            Globals.SBUSused = false;
            Globals.DBUSALUused = false;
            Globals.SBUSALUused = false;
            Globals.SPused = false;
            Globals.FLAGused = false;
            Globals.RBUSFLAGused = false;
            Globals.Tused = false;
            Globals.IVRused = false;
            Globals.ADRused = false;
            Globals.MDRused = false;
            Globals.MARused = false;
            Globals.PCused = false;
            Globals.RBUSRGused = false;
            Globals.RGused = false;
            Globals.RBUSSPused = false;
            Globals.RBUSTused = false;
            Globals.RBUSADRused = false;
            Globals.RBUSMDRused = false;
            Globals.RBUSMARused = false;
            Globals.SBUSFLAGused = false;
            Globals.SBUSRGused = false;
            Globals.SBUSSPused = false;
            Globals.SBUSTused = false;
            Globals.SBUSADRused = false;
            Globals.SBUSMDRused = false;
            Globals.SBUSMARused = false;
            Globals.SBUSPCused = false;
            Globals.SBUSIVRused = false;
            Globals.DBUSFLAGused = false;
            Globals.DBUSRGused = false;
            Globals.DBUSSPused = false;
            Globals.DBUSTused = false;
            Globals.DBUSADRused = false;
            Globals.DBUSMDRused = false;
            Globals.DBUSMARused = false;
            Globals.DBUSPCused = false;
            Globals.DBUSIVRused = false;

        }
    }
}
