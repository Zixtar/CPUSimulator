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
        public TwoOperatorInstruction(string text) 
        {
            TextForm = text;
        }
        public string TextForm { get; }
        public int RD => BinaryForm & 0x3;
        public int MAD => (BinaryForm & 0x4) >> 4;
        public int RS => (BinaryForm & 0x18) >> 6;
        public int MAS => (BinaryForm & 0x20) >> 10;
        public int OPCODE => (BinaryForm & 0xC0) >> 12;

        private int? _binaryFrom;

        public int BinaryForm
        {
            get => GetBinaryForm();
            private set
            {
                _binaryFrom = value;
            }
        }

        public int GetBinaryForm()
        {
            return _binaryFrom ?? GenerateBinaryForm();
        }

        public int GenerateBinaryForm()
        {
            throw new NotImplementedException();
        }
    }
}
