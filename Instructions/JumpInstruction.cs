using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUSimulator.Instructions
{
    internal class JumpInstruction : Instruction
    {
        private static readonly Dictionary<string, int> InstructionOpcodes = new Dictionary<string, int>()
        {
            {"BEQ",0x300},
            {"BNE",0x304},
            {"BMI",0x308},
            {"BPL",0x30C},
            {"BCS",0x310},
            {"BCC",0x314},
            {"BVS",0x318},
            {"BVC",0x31C},
            {"JMP",0x320},
            {"CALL",0x324},
        };
        public JumpInstruction(string text) : base(text)
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
                if (char.IsLetter(InstructionParts[1].First()))
                {
                    list.Add(0);
                    return list;
                }
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
