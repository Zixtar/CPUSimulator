using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUSimulator.Instructions
{
    internal class JumpInstruction : IInstruction
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
        public JumpInstruction(string text)
        {
            TextForm = text;
        }
        public string TextForm { get; }

        private int? _binaryForm;

        public int BinaryForm
        {
            get => GetBinaryForm();
            private set
            {
                _binaryForm = value;
            }
        }

        public int OFFSET => BinaryForm & 0xFF;
        public int OPCODE => (BinaryForm & 0xFF00) >> 8;

        public int GetBinaryForm()
        {
            return _binaryForm ?? GenerateBinaryForm();
        }

        public int GenerateBinaryForm()
        {
            var InstructionParts = TextForm.Replace(',', ' ').Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var opcode = InstructionOpcodes[InstructionParts.First()];
            return opcode << 8;
        }
    }
}
