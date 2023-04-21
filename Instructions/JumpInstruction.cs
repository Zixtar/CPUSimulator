using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUSimulator.Instructions
{
    internal class JumpInstruction : Instruction
    {
        private Dictionary<string, int> InstructionOpcodes = new Dictionary<string, int>()
        {
            {"BR",0xC0},
            {"BNE",0xC2},
            {"BEQ",0xC2},
            {"BPL",0xC3},
            {"BMI",0xC4},
            {"BCS",0xC5},
            {"BCC",0xC6},
            {"BVS",0xC7},
            {"BVC",0xC8}
        };
        public JumpInstruction(string text) :base(text)
        {

        }


        public override List<int> GenerateBinaryForm()
        {
            var list = new List<int>();
            var InstructionParts = TextForm.Replace(',', ' ').Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var opcode = InstructionOpcodes[InstructionParts.First()];
            var binaryForm = opcode << 8;
            list.Add(binaryForm);
            return list;
        }
    }
}
