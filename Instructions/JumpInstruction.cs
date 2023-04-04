using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUSimulator.Instructions
{
    internal class JumpInstruction : IInstruction
    {
        public JumpInstruction(string text)
        {
            TextForm = text;
        }
        public string TextForm { get; }

        private int? _binaryFrom;

        public int BinaryForm
        {
            get => GetBinaryForm();
            private set
            {
                _binaryFrom = value;
            }
        }

        public int OFFSET => BinaryForm & 0xF;
        public int OPCODE => (BinaryForm & 0xF0) >> 8;

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
