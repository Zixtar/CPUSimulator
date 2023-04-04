using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUSimulator.Instructions
{
    internal class OneOperatorInstruction : IInstruction
    {
        private Dictionary<string, int> InstructionOpcodes = new Dictionary<string, int>()
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
        public OneOperatorInstruction(string text)
        {
            TextForm = text;
        }
        public string TextForm { get; }

        private int? _binaryForm;
        public int RD => BinaryForm & 0xF;
        public int MAD => (BinaryForm & 0x30) >> 4;
        public int OPCODE => (BinaryForm & 0xFFC0) >> 6;

        public int BinaryForm
        {
            get => GetBinaryForm();
            private set
            {
                _binaryForm = value;
            }
        }

        public int GetBinaryForm()
        {
            return _binaryForm ?? GenerateBinaryForm();
        }

        public int GenerateBinaryForm()
        {
            var InstructionParts = TextForm.Replace(',', ' ').Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var opcode = InstructionOpcodes[InstructionParts.First()];
            return opcode << 6;
        }    
    }
}
