using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUSimulator.Instructions
{
    internal class SpecialInstruction : Instruction
    {
        private static readonly Dictionary<string, int> InstructionOpcodes = new Dictionary<string, int>()
        {
            {"CLC",0xE0F7},
            {"CLV",0xE0FB},
            {"CLZ",0xE0FD},
            {"CLS",0xE0FE},
            {"CCC",0xE0F0},
            {"SEC",0xE108},
            {"SEV",0xE104},
            {"SEZ",0xE102},
            {"SES",0xE101},
            {"SCC",0xE10F},
            {"NOP",0xE200},
            {"HALT",0xE300},
            {"EI",0xE400},
            {"DI",0xE500},
            {"PUSH PC",0xE600},
            {"POP PC",0xF700},
            {"PUSH FLAG",0xF800},
            {"POP FLAG",0xF900},
            {"RET",0xFA00},
            {"RETI",0xFB00}
        };
        public SpecialInstruction(string text) : base(text)
        {

        }

        public override List<int> GenerateBinaryForm()
        {
            var list = new List<int>();
            var InstructionParts = TextForm.Replace(',', ' ').Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int opcode;
            if (InstructionParts.Count()>1 && (InstructionParts[1] == "PC" || InstructionParts[1] == "FLAG"))
            {
                opcode = InstructionOpcodes[InstructionParts.First() + " " + InstructionParts[1]];
            }
            else 
            {
                opcode = InstructionOpcodes[InstructionParts.First()];
            }
            list.Add(opcode);

            return list;
        }
    }
}
