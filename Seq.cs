using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static CPUSimulator.Globals;

namespace CPUSimulator
{
    internal class Seq
    {
        int stare = -1;
        Int64 MIR, MAR;
        uint bitsSuccesor => (uint)((MIR & (long)MastiMIR.succ) >> 11);
        uint binarIndex => (uint)((MIR & (long)MastiMIR.index) >> 8);
        int tf => Convert.ToInt16((MIR & (long)MastiMIR.tf) > 0);
        public Seq()
        {
            MIR = 0;
        }

        public void StartSeq()
        {
            stare = 1;
        }

        private void DoCore()
        {
            var indexes = new int[8];
            switch (stare)
            {
                case 0:
                    MIR = MPM[MAR];
                    stare = 1;
                    break;
                case 1:

                    var index = CalcIndex();
                    var microadresa = MIR & (long)MastiMIR.adr;
                    var g = Compute_g();
                    if (g) MAR = microadresa + index;
                    else MAR++;

                    if (!Convert.ToBoolean(IR & (long)MastiMIR.bit24) & !Convert.ToBoolean(IR & (long)MastiMIR.bit25))
                    {
                        stare = 0;
                        break;  //am inteles ok?
                    }
                    else
                    {
                        stare = 2;
                    }
                    //!!!!!!!!!!!!!!NU inteleg ce trebuie sa fac aici!!!!!!!!!!!!!!!!!
                    //aici se decodifica toate campurile din MIR, mai putin MemOp, si se executa fiecare microcomanda
                    //vom avea cate un switch/case pentru fiecare camp!!!
                    var sbus = (uint)((MIR & (long)MastiMIR.sbus) >> 32);  //posibil sa crape nush daca e logic sau arithmetic shift right
                    var dbus = (uint)((MIR & (long)MastiMIR.dbus) >> 28);
                    var alu = (uint)((MIR & (long)MastiMIR.alu) >> 24);
                    var rbus = (uint)((MIR & (long)MastiMIR.rbus) >> 20);
                    var mem = (uint)((MIR & (long)MastiMIR.mem) >> 18);
                    var oth = (uint)((MIR & (long)MastiMIR.oth) >> 14);
                    ComputeSBUS(sbus);
                    ComputeDBUS(dbus);
                    ComputeALU(alu);
                    ComputeRBUS(rbus);
                    ComputeOth(oth);
                    break;
                case 2:
                    break;
                case 3:
                    //aici se decodifica campul MemOp, si se executa fiecare microcomanda
                    //vom avea un switch/case pentru MemOp!!!
                    break;
            }
            #region All_the_switches_in_the_world
            int CalcIndex()
            {
                switch (binarIndex)
                {
                    case 0:
                        return 0;
                    case 1: //clasele merg 0-3 sau 1-4 ??
                        {
                            if ((IR & (int)MastiIR.bit1) == 0) return 0;

                            if ((IR & (int)MastiIR.bit2) == 0) return 1;

                            if ((IR & (int)MastiIR.bit3) == 0) return 2;

                            return 3;
                        }
                    case 2: return (IR & (int)MastiIR.MAS) >> 10;

                    case 3: return (IR & (int)MastiIR.MAD) >> 4;

                    case 4: return (IR & (int)MastiIR.OPA) >> 12;

                    case 5: return (IR & (int)MastiIR.OPBCD) >> 8;

                    case 6: return (IR & (int)MastiIR.OPBCD) >> 7;

                        //case 7: return IDK
                }
                throw new Exception("Ce cautam p-aici?");
            }
            void ComputeSBUS(uint sbus)  //teoretic cred ca ar trb sa pun niste minuni care pregatesc astea si abia mai incolo sa ajunga propriuzis pe SBUS dar na
            {
                switch (sbus)
                {
                    case 0:
                        break;
                    case 1:
                        {
                            SBUS = FLAG;
                            break;
                        }
                    case 2:
                        {
                            SBUS = R[(IR & (int)MastiIR.RS) >> 6];
                            break;
                        }
                    case 3:
                        {
                            SBUS = SP;
                            break;
                        }
                    case 4:
                        {
                            SBUS = T;
                            break;
                        }
                    case 5:
                        {
                            SBUS = (short)~(int)T; //bomba conversia csz
                            break;
                        }
                    case 6:
                        {
                            SBUS = PC;
                            break;
                        }
                    case 7:
                        {
                            SBUS = IVR;
                            break;
                        }
                    case 8:
                        {
                            SBUS = ADR;
                            break;
                        }
                    case 9:
                        {
                            SBUS = MDR;
                            break;
                        }
                    case 10:
                        {
                            SBUS = (short)(IR << 24 >> 24); //IR[7:0]
                            break;
                        }
                    case 11:
                        {
                            SBUS = 0;
                            break;
                        }
                    case 12:
                        {
                            SBUS = -1;
                            break;
                        }
                }
            }
            void ComputeDBUS(uint dbus)  //teoretic cred ca ar trb sa pun niste minuni care pregatesc astea si abia mai incolo sa ajunga propriuzis pe SBUS dar na
            {
                switch (dbus)
                {
                    case 0:
                        break;
                    case 1:
                        {
                            DBUS = FLAG;
                            break;
                        }
                    case 2:
                        {
                            DBUS = R[(IR & (int)MastiIR.RD) >> 6]; //nush daca RG la astea se refera
                            break;
                        }
                    case 3:
                        {
                            DBUS = SP;
                            break;
                        }
                    case 4:
                        {
                            DBUS = T;
                            break;
                        }
                    case 5:
                        {
                            DBUS = PC;
                            break;
                        }
                    case 6:
                        {
                            DBUS = IVR;
                            break;
                        }
                    case 7:
                        {
                            DBUS = ADR;
                            break;
                        }
                    case 8:
                        {
                            DBUS = MDR;
                            break;
                        }
                    case 9:
                        {
                            DBUS = (short)~MDR;
                            break;
                        }
                    case 10:
                        {
                            DBUS = (short)(IR << 24 >> 24); //IR[7:0]
                            break;
                        }
                    case 11:
                        {
                            DBUS = 0;
                            break;
                        }
                    case 12:
                        {
                            DBUS = -1;
                            break;
                        }
                }
            }//copy-paste sper ca am schimbat ok
            void ComputeALU(uint alu)
            {

            }
            void ComputeRBUS(uint rbus)  //teoretic cred ca ar trb sa pun niste minuni care pregatesc astea si abia mai incolo sa ajunga propriuzis pe SBUS dar na
            {
                switch (rbus)
                {
                    case 0:
                        break;
                    case 1:
                        {
                            FLAG = RBUS;
                            break;
                        }
                    case 2:
                        {
                            FLAG &= 0b111111111111000; //dc ma lasa doar pe 15....
                            FLAG += (short)(RBUS & 0b111111111111000);
                            break;
                        }
                    case 3:
                        {
                            R[(IR & (int)MastiIR.RD) >> 6] = RBUS;
                            break;
                        }
                    case 4:
                        {
                            SP = RBUS;
                            break;
                        }
                    case 5:
                        {
                            T = RBUS;
                            break;
                        }
                    case 6:
                        {
                            PC = RBUS;
                            break;
                        }
                    case 7:
                        {
                            IVR = RBUS;
                            break;
                        }
                    case 8:
                        {
                            ADR = RBUS;
                            break;
                        }
                    case 9:
                        {
                            MDR = RBUS;
                            break;
                        }
                }
            }//copy-paste sper ca am schimbat ok
            void ComputeOth(uint oth)
            {
                switch (oth)
                {
                    case 0:
                        break;
                    case 1:
                        {
                            SP += 2;
                            break;
                        }
                    case 2:
                        {
                            SP -= 2;
                            break;
                        }
                    case 3:
                        {
                            PC += 2;
                            break;
                        }
                    case 4:
                        {
                            BE[0] = true;
                            break;
                        }
                    case 5:
                        {
                            BE[1] = true; //e ok cu bistabilii?
                            break;
                        }
                    case 6:
                        {
                            //apai sa ma bata
                            break;
                        }
                    case 7:
                        {
                           //ca nu am alu facut
                            break;
                        }
                    case 8:
                        {
                            // :(
                            break;
                        }
                        case 9:
                        {
                            BVI = true;
                            break;
                        }
                    case 10:
                        {
                            BVI = false;
                            break;
                        }
                    case 11:
                        {
                            BPO = false;
                            break;
                        }
                    case 12:
                        {
                            INTA = 1; //registru sau bistabil?
                            SP -= 2;
                            break;
                        }
                        case 13:
                        {
                            BE[0] = false;
                            BE[1] = false;
                            BI = false; 
                            break;
                        }
                }
            }
            #endregion All_the_switches_in_the_world
        }


        private bool Compute_g()
        {
            switch (bitsSuccesor)
            {
                case 0:
                    return false;
                case 1:
                    return true;
                case 2:
                    return (Convert.ToInt16(BE[0]) ^ tf) > 0;
                case 3:
                    return (Convert.ToInt16(BE[1]) ^ tf) > 0; 
                case 4:  //nush cum sa pun flags ca sa fie relevant pt microprogram sau macar daca trebuie
                    return false;
                case 5:  //nush cum sa pun flags ca sa fie relevant pt microprogram sau macar daca trebuie
                    return false;
                case 6:  //nush cum sa pun flags ca sa fie relevant pt microprogram sau macar daca trebuie
                    return false;
                case 7:  //nush cum sa pun flags ca sa fie relevant pt microprogram sau macar daca trebuie, edit: cred ca sunt doar prost si nu inteleg ceva, edit2: am inteles ce trb facut, facem alta data ca nu mai pot
                    return false;
                default:
                    return false;
            }

        }

        #pragma warning disable format
        enum MastiMIR : long
        {
            sbus =  0b111100000000000000000000000000000000,
            dbus =  0b000011110000000000000000000000000000,
            alu =   0b000000001111000000000000000000000000,
            rbus =  0b000000000000111100000000000000000000,
            mem =   0b000000000000000011000000000000000000,
            oth =   0b000000000000000000111100000000000000,
            succ =  0b000000000000000000000011100000000000,
            index = 0b000000000000000000000000011100000000,
            tf =    0b000000000000000000000000000010000000,
            adr =   0b000000000000000000000000000001111111,
            bit24 = 0b000000000001000000000000000000000000,
            bit25 = 0b000000000010000000000000000000000000,

        }
        enum MastiIR : int
        {
            MAS =    0b0000110000000000,
            MAD =    0b0000000000110000,
            OPA =    0b0111000000000000,
            OPBCD =  0b0000111100000000,
            RS =     0b0000001111000000,
            RD =     0b0000000000001111,
            bit1 =   0b1000000000000000,
            bit2 =   0b0100000000000000,
            bit3 =   0b0010000000000000,
        }
        #pragma warning restore format
    }
}
