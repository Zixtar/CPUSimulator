using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static CPUSimulator.Globals;
// ReSharper disable All

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
                        break; //am inteles ok?
                    }
                    else
                    {
                        stare = 2;
                    }

                    var sbus = (ActionsSBUS)((MIR & (long)MastiMIR.sbus) >> 32); //posibil sa crape nush daca e logic sau arithmetic shift right
                    var dbus = (ActionsDBUS)((MIR & (long)MastiMIR.dbus) >> 28);
                    var alu = (ActionsALU)((MIR & (long)MastiMIR.alu) >> 24);
                    var rbus = (ActionsRBUS)((MIR & (long)MastiMIR.rbus) >> 20);
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
                    var mem = (ActionsMEM)((MIR & (long)MastiMIR.mem) >> 18);
                    DoMem(mem);
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

            void ComputeSBUS(ActionsSBUS sbus)
            {
                switch (sbus)
                {
                    case ActionsSBUS.NONE:
                        break;
                    case ActionsSBUS.PdFLAG:
                        {
                            SBUS = FLAG;
                            break;
                        }
                    case ActionsSBUS.PdRG:
                        {
                            SBUS = R[(IR & (int)MastiIR.RS) >> 6];
                            break;
                        }
                    case ActionsSBUS.PdSP:
                        {
                            SBUS = SP;
                            break;
                        }
                    case ActionsSBUS.PdT:
                        {
                            SBUS = T;
                            break;
                        }
                    case ActionsSBUS.PdnT:
                        {
                            SBUS = (short)~(int)T; //bomba conversia csz
                            break;
                        }
                    case ActionsSBUS.PdPC:
                        {
                            SBUS = PC;
                            break;
                        }
                    case ActionsSBUS.PdIVR:
                        {
                            SBUS = IVR;
                            break;
                        }
                    case ActionsSBUS.PdADR:
                        {
                            SBUS = ADR;
                            break;
                        }
                    case ActionsSBUS.PdMDR:
                        {
                            SBUS = MDR;
                            break;
                        }
                    case ActionsSBUS.PdIR07:
                        {
                            SBUS = (short)(IR << 24 >> 24); //IR[7:0]
                            break;
                        }
                    case ActionsSBUS.Pd0:
                        {
                            SBUS = 0;
                            break;
                        }
                    case ActionsSBUS.Pdn1:
                        {
                            SBUS = -1;
                            break;
                        }
                }
            }

            void ComputeDBUS(ActionsDBUS dbus)
            {
                switch (dbus)
                {
                    case ActionsDBUS.NONE:
                        break;
                    case ActionsDBUS.PdFLAG:
                        {
                            DBUS = FLAG;
                            break;
                        }
                    case ActionsDBUS.PdRG:
                        {
                            DBUS = R[(IR & (int)MastiIR.RD) >> 6]; //nush daca RG la astea se refera
                            break;
                        }
                    case ActionsDBUS.PdSP:
                        {
                            DBUS = SP;
                            break;
                        }
                    case ActionsDBUS.PdT:
                        {
                            DBUS = T;
                            break;
                        }
                    case ActionsDBUS.PdPC:
                        {
                            DBUS = PC;
                            break;
                        }
                    case ActionsDBUS.PdIVR:
                        {
                            DBUS = IVR;
                            break;
                        }
                    case ActionsDBUS.PdADR:
                        {
                            DBUS = ADR;
                            break;
                        }
                    case ActionsDBUS.PdMDR:
                        {
                            DBUS = MDR;
                            break;
                        }
                    case ActionsDBUS.PdnMDR:
                        {
                            DBUS = (short)~MDR;
                            break;
                        }
                    case ActionsDBUS.PdIR:
                        {
                            DBUS = (short)(IR << 24 >> 24); //IR[7:0]
                            break;
                        }
                    case ActionsDBUS.Pd0:
                        {
                            DBUS = 0;
                            break;
                        }
                    case ActionsDBUS.Pdn1:
                        {
                            DBUS = -1;
                            break;
                        }
                }
            } //copy-paste sper ca am schimbat ok

            void ComputeALU(ActionsALU alu)
            {
                switch (alu) //TODO implement flags nici nu prea inteleg ar trb ceva intermediar si abia la PdCOND sa intre in flags sau ala e ceva pt cazuri speciale
                {
                    case ActionsALU.NONE:
                        {
                            break;
                        }
                    case ActionsALU.SBUS:
                        {
                            RBUS = SBUS;
                            break;
                        }
                    case ActionsALU.DBUS:
                        {
                            RBUS = DBUS;
                            break;
                        }
                    case ActionsALU.ADD:
                        {
                            RBUS = (short)(SBUS + DBUS);
                            break;
                        }
                    case ActionsALU.SUB:
                        {
                            RBUS = (short)(SBUS - DBUS);
                            break;
                        }
                    case ActionsALU.AND:
                        {
                            RBUS = (short)(SBUS & DBUS);
                            break;
                        }
                    case ActionsALU.OR:
                        {
                            RBUS = (short)(SBUS | DBUS);
                            break;
                        }
                    case ActionsALU.XOR:
                        {
                            RBUS = (short)(SBUS ^ DBUS);
                            break;
                        }
                    case ActionsALU.ASL:
                        {
                            RBUS = (short)(SBUS << DBUS);
                            break;
                        }
                    case ActionsALU.ASR:
                        {
                            RBUS = (short)(SBUS >>
                                           DBUS); //Imi e frica ca asta posibil converteste la int prima data si nu face arithmetic shift ca pune 0 in fata
                            break;
                        }
                    case ActionsALU.LSR:
                        {
                            //RBUS = (short)(SBUS >>> DBUS); TODO: daca facem update la C#11 avem operatorul >>>
                            break;
                        }
                    case ActionsALU.ROL:
                        {
                            RBUS = SBUS;
                            for (int i = 0; i < DBUS; i++)
                            {
                                if ((RBUS & 0b1000000000000000) > 0)
                                {
                                    RBUS = (short)(RBUS * 2 + 1);
                                }

                                RBUS = (short)(RBUS << 1);
                            }

                            break;
                        }
                    case ActionsALU.ROR:
                        {
                            RBUS = SBUS;
                            for (int i = 0; i < DBUS; i++)
                            {
                                if ((RBUS & 0b0000000000000001) > 0)
                                {
                                    RBUS = (short)((RBUS / 2) | 0b1000000000000000);
                                }

                                RBUS /= 2;
                            }

                            break;
                        }
                    case ActionsALU.RLC:
                        {
                            RBUS = SBUS;
                            for (int i = 0; i < DBUS; i++)
                            {
                                if ((RBUS & 0b1000000000000000) > 0)
                                {
                                    FLAG |= (int)MastiFLAG.Carry; //pune carry pe 1
                                }
                                else
                                {
                                    FLAG &= ~(int)MastiFLAG.Carry; //pune carry pe 0
                                }

                                RBUS = (short)(RBUS * 2 + (FLAG & (int)MastiFLAG.Carry));
                            }

                            break;
                        }
                    case ActionsALU.RRC:
                        {
                            RBUS = SBUS;
                            for (int i = 0; i < DBUS; i++)
                            {
                                if ((RBUS & 0b0000000000000001) > 0)
                                {
                                    FLAG |= (int)MastiFLAG.Carry; //pune carry pe 1
                                }
                                else
                                {
                                    FLAG &= ~(int)MastiFLAG.Carry; //pune carry pe 0
                                }

                                RBUS = (short)(RBUS / 2 + ((FLAG & (int)MastiFLAG.Carry) << 15)); //e ok cu 15?
                            }

                            break;
                        }
                }

            }

            void ComputeRBUS(ActionsRBUS rbus)
            {
                switch (rbus)
                {
                    case ActionsRBUS.NONE:
                        break;
                    case ActionsRBUS.PmFLAG:
                        {
                            FLAG = RBUS;
                            break;
                        }
                    case ActionsRBUS.PmFLAG30:
                        {
                            FLAG &= 0b111111111111000; //dc ma lasa doar pe 15....
                            FLAG += (short)(RBUS & 0b111111111111000);
                            break;
                        }
                    case ActionsRBUS.PmRG:
                        {
                            R[(IR & (int)MastiIR.RD) >> 6] = RBUS;
                            break;
                        }
                    case ActionsRBUS.PmSP:
                        {
                            SP = RBUS;
                            break;
                        }
                    case ActionsRBUS.PmT:
                        {
                            T = RBUS;
                            break;
                        }
                    case ActionsRBUS.PmPC:
                        {
                            PC = RBUS;
                            break;
                        }
                    case ActionsRBUS.PmIVR:
                        {
                            IVR = RBUS;
                            break;
                        }
                    case ActionsRBUS.PmADR:
                        {
                            ADR = RBUS;
                            break;
                        }
                    case ActionsRBUS.PmMDR:
                        {
                            MDR = RBUS;
                            break;
                        }
                }
            } //copy-paste sper ca am schimbat ok

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

            void DoMem(ActionsMEM mem) //TODO this
            {
                switch (mem)
                {
                    case ActionsMEM.NONE:
                        break;
                    case ActionsMEM.IFCH:
                    {
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
                case 4: //nush cum sa pun flags ca sa fie relevant pt microprogram sau macar daca trebuie
                    return false;
                case 5: //nush cum sa pun flags ca sa fie relevant pt microprogram sau macar daca trebuie
                    return false;
                case 6: //nush cum sa pun flags ca sa fie relevant pt microprogram sau macar daca trebuie
                    return false;
                case 7
                    : //nush cum sa pun flags ca sa fie relevant pt microprogram sau macar daca trebuie, edit: cred ca sunt doar prost si nu inteleg ceva, edit2: am inteles ce trb facut, facem alta data ca nu mai pot
                    return false;
                default:
                    return false;
            }

        }

        #region Masks
 #pragma warning disable format

        enum MastiMIR : long
        {
            sbus  = 0b111100000000000000000000000000000000,
            dbus  = 0b000011110000000000000000000000000000,
            alu   = 0b000000001111000000000000000000000000,
            rbus  = 0b000000000000111100000000000000000000,
            mem   = 0b000000000000000011000000000000000000,
            oth   = 0b000000000000000000111100000000000000,
            succ  = 0b000000000000000000000011100000000000,
            index = 0b000000000000000000000000011100000000,
            tf    = 0b000000000000000000000000000010000000,
            adr   = 0b000000000000000000000000000001111111,
            bit24 = 0b000000000001000000000000000000000000,
            bit25 = 0b000000000010000000000000000000000000,

        }

        enum MastiIR : int
        {
            MAS   = 0b0000110000000000,
            MAD   = 0b0000000000110000,
            OPA   = 0b0111000000000000,
            OPBCD = 0b0000111100000000,
            RS    = 0b0000001111000000,
            RD    = 0b0000000000001111,
            bit1  = 0b1000000000000000,
            bit2  = 0b0100000000000000,
            bit3  = 0b0010000000000000,
        }

        enum MastiFLAG
        {
            Carry = 0b0000000000000001,
            Zero  = 0b0000000000000010,
            Sign  = 0b0000000000000100,
            Ovf   = 0b0000000000001000,
        }
#pragma warning restore format

        #endregion

        #region Actions
        enum ActionsDBUS
        {
            NONE, PdFLAG, PdRG, PdSP, PdT, PdPC, PdIVR, PdADR, PdMDR, PdnMDR, PdIR, Pd0, Pdn1
        }
        enum ActionsSBUS
        {
            NONE, PdFLAG, PdRG, PdSP, PdT, PdnT, PdPC, PdIVR, PdADR, PdMDR, PdIR07, Pd0, Pdn1
        }
        enum ActionsALU
        {
            NONE, SBUS, DBUS, ADD, SUB, AND, OR, XOR, ASL, ASR, LSR, ROL, ROR, RLC, RRC
        }
        enum ActionsRBUS
        {
            NONE, PmFLAG, PmFLAG30, PmRG, PmSP, PmT, PmPC, PmIVR, PmADR, PmMDR
        }

        enum ActionsMEM
        {
            NONE, IFCH, RD, WR
        }

        #endregion

    }
}

