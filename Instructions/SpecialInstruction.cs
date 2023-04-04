using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUSimulator.Instructions
{
    internal class SpecialInstruction : IInstruction
    {
        public SpecialInstruction(string text) 
        {
            TextForm = text;
        }
        public string TextForm { get; }
        public int OPCODE => BinaryForm;

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
