using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUSimulator.Instructions
{
    internal class SpecialInstruction : IInstruction
    {
        private Dictionary<string, int> InstructionOpcodes = new Dictionary<string, int>()
        {
            {"NOP",0xE000},
            {"RET",0xE001},
            {"RETI",0xE002},
            {"HALT",0xE003},
            {"WAIT",0xE004},
            {"PUSH PC",0xE005},
            {"POP PC",0xE006},
            {"PUSH FLAG",0xE007},
            {"POP FLAG",0xE008},
            {"CLC",0xE009},
            {"CLV",0xE00A},
            {"CLZ",0xE00B},
            {"CLS",0xE00C},
            {"CCC",0xE00D},
            {"SEC",0xE00E},
            {"SEV",0xE00F},
            {"SEZ",0xE010},
            {"SES",0xE011},
            {"SCC",0xE012}
        };
        public SpecialInstruction(string text) 
        {
            TextForm = text;
        }
        public string TextForm { get; }
        public int OPCODE => BinaryForm;

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
            int opcode;
            if (InstructionParts[1] == "PC" || InstructionParts[1] == "FLAG")
            {
                opcode = InstructionOpcodes[InstructionParts.First() + " " + InstructionParts[1]];
            }
            else 
            {
                opcode = InstructionOpcodes[InstructionParts.First()];
            }

            return opcode;
        }
    }
}
