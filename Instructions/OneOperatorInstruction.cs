using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUSimulator.Instructions
{
    internal class OneOperatorInstruction : Instruction
    {
        private static readonly Dictionary<string, int> InstructionOpcodes = new Dictionary<string, int>()
        {
            {"CLR",0x200},
            {"NEG",0x201},
            {"INC",0x202},
            {"DEC",0x203},
            {"ASL",0x204},
            {"ASR",0x205},
            {"LSR",0x206},
            {"ROL",0x207},
            {"ROR",0x208},
            {"RLC",0x209},
            {"RRC",0x20A},
            {"JMP",0x20B},
            {"CALL",0x20C},
            {"PUSH",0x20D},
            {"POP",0x20E}
        };
        public OneOperatorInstruction(string text) : base(text)
        {
        }


        public override List<int> GenerateBinaryForm()
        {
            var list = new List<int>();
            var InstructionParts = TextForm.Replace(',', ' ').Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var opcode = InstructionOpcodes[InstructionParts.First()];
            list.Add(opcode << 6);
            if (InstructionParts[1].First() == 'R')
            {
                list[0] += 1 << 4;
                list[0] += int.Parse(InstructionParts[1].Substring(1));
            }
            else if (InstructionParts[1].First() == '(')
            {
                list[0] += 2 << 4;
                list[0] += int.Parse(InstructionParts[1]
                    .Split("()".ToCharArray())
                    .Where(x => x != string.Empty)
                    .First().Substring(1));
            }
            else
            {
                var parts = InstructionParts[1]
                    .Split("()".ToCharArray());
                if (parts.Length != 1)
                {
                    list[0] += int.Parse(parts
                        .Where(x => x.First() == 'R')
                        .First().Substring(1));
                    list[0] += 3 << 4;
                }
                list.AddRange(parts.Where(x => x != string.Empty && char.IsNumber(x.First())
                    && char.IsNumber(x.Last()))
                    .Select(x => int.Parse(x)));
                list.AddRange(parts.Where(x => x != string.Empty && x.Last() == 'H')
                    .Select(x => x.Substring(0, x.Length - 1)).Select(x => Convert.ToInt32(x, 16)));
            }
            return list;
        }
    }
}
