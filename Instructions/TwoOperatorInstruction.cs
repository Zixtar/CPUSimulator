using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CPUSimulator.Instructions
{
    internal class TwoOperatorInstruction : Instruction
    {
        private static readonly Dictionary<string,int> InstructionOpcodes=new Dictionary<string,int>()
        {
            {"MOV",0x0},
            {"ADD",0x1},
            {"SUB",0x2},
            {"CMP",0x3},
            {"AND",0x4},
            {"OR",0x5},
            {"XOR",0x6}
        };
        public TwoOperatorInstruction(string text) :base(text)
        {

        }

        public override List<int> GenerateBinaryForm()
        {
            var list = new List<int>();
            var InstructionParts = TextForm.Replace(',', ' ').Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var opcode = InstructionOpcodes[InstructionParts.First()];
            list.Add(opcode << 12);
            ParseRegister(list, InstructionParts,1);
            ParseRegister(list, InstructionParts,2);
            return list;
        }

        private static void ParseRegister(List<int> list, string[] InstructionParts, int index)
        {
            int shiftAdressingMode = index == 1 ? 4 : 10;
            int shiftRegister = index == 1 ? 0 : 6;
            if (InstructionParts[index].First() == 'R')
            {
                list[0] += 1 << shiftAdressingMode;
                list[0] += int.Parse(InstructionParts[index].Substring(1)) << shiftRegister;
            }
            else if (InstructionParts[index].First() == '(')
            {
                list[0] += 2 << shiftAdressingMode;
                list[0] += int.Parse(InstructionParts[index]
                    .Split("()".ToCharArray())
                    .Where(x=>x!=string.Empty)
                    .First().Substring(1))<< shiftRegister;
            }
            else
            {
                var parts = InstructionParts[index]
                    .Split("()".ToCharArray());
                if(parts.Length!=1)
                {
                    list[0] += int.Parse(parts
                        .Where(x => x.First() == 'R')
                        .First().Substring(1)) << shiftRegister;
                    list[0] += 3 << shiftAdressingMode;
                }
                list.AddRange(parts.Where(x=>x!=string.Empty 
                    && char.IsNumber(x.First()))
                    .Select(x=>int.Parse(x)));
                list.AddRange(parts.Where(x=>x!=string.Empty && x.Last()=='H')
                    .Select(x=>x.Substring(0,x.Length-1)).Select(x=>Convert.ToInt32(x,16)));
            }
        }
    }
}
