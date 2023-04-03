using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUSimulator.Instructions
{
    internal class OneOperatorInstruction : IInstuction
    {
        public OneOperatorInstruction(string text)
        {
            TextForm = text;
        }
        public string TextForm { get; }

        private int? _binaryFrom;
        public int RD => BinaryForm & 0x3;
        public int MAD => (BinaryForm & 0x4) >> 4;
        public int OPCODE => (BinaryForm & 0xF8) >> 6;

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
