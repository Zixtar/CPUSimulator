using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUSimulator.Instructions
{
    internal static class InstructionFactory
    {
        private static string[] JmpText = {"BNE", "BEQ", "BPL", "BMI", "BCS", "BCC", "BVS", "BVC", "JMP", "CALL"};
        public static Instruction GetInstruction(string text)
        {
            var type = GetInstructionType(text);

            switch (type)
            {
                case InstructionType.TwoOperators:
                    {
                        return new TwoOperatorInstruction(text);
                    }
                case InstructionType.OneOperator:
                    {
                        return new OneOperatorInstruction(text);
                    }
                case InstructionType.Jump:
                    {
                        return new JumpInstruction(text);
                    }
                case InstructionType.Special:
                    {
                        return new SpecialInstruction(text);
                    }
                default:
                    {
                        return null;
                    }
            }

        }

        private static InstructionType GetInstructionType(string text)
        {
            var InstructionParts = text.Replace(',', ' ').Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (InstructionParts.Length == 3) return InstructionType.TwoOperators;
            if (InstructionParts.Length == 2)
            {
                if (InstructionParts[1] == "PC" || InstructionParts[1] == "FLAG") return InstructionType.Special;
                if (JmpText.Contains(InstructionParts[0])) return InstructionType.Jump;
                return InstructionType.OneOperator;
            }
            return InstructionType.Special;
        }


        public enum InstructionType
        {
            TwoOperators,
            OneOperator,
            Jump,
            Special
        }
    }
}
