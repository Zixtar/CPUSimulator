using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CPUSimulator.Instructions
{
    internal class TwoOperatorInstruction : IInstruction
    {
        private Dictionary<string,int> InstructionOpcodes=new Dictionary<string,int>()
        {
            {"MOV",0x0},
            {"ADD",0x1},
            {"SUB",0x2},
            {"CMP",0x3},
            {"AND",0x4},
            {"OR",0x5},
            {"XOR",0x6}
        };
        public TwoOperatorInstruction(string text) 
        {
            TextForm = text;
        }
        public string TextForm { get; }
        public int RD => BinaryForm & 0xF;
        public int MAD => (BinaryForm & 0x30) >> 4;
        public int RS => (BinaryForm & 0x3C0) >> 6;
        public int MAS => (BinaryForm & 0xC00) >> 10;
        public int OPCODE => (BinaryForm & 0xF000) >> 12;

        private int? _binaryForm;

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
            return opcode<<12;
        }
    }
}
