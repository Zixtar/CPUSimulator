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
            {"CLR",0x280},
            {"NEG",0x284},
            {"INC",0x288},
            {"DEC",0x28C},
            {"ASL",0x290},
            {"ASR",0x294},
            {"LSR",0x298},
            {"ROL",0x29C},
            {"ROR",0x2A0},
            {"RLC",0x2A4},
            {"RRC",0x2A8},
            {"PUSH",0x2AC},
            {"POP",0x2B0}
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
