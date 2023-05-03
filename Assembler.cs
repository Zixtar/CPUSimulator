using CPUSimulator.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CPUSimulator
{
    public class Assembler
    {
        public List<int> ParseInstructionList(List<string> instructionList)
        {
            List<int> result = new List<int>();
            List<Tuple<int, JumpInstruction>> instructionToChange = new();
            foreach (var instructionText in instructionList)
            {
                var trimmedInstruction = instructionText.ReplaceLineEndings().Trim().Split(";").First();
                if (string.IsNullOrEmpty(trimmedInstruction)) continue;
                if (trimmedInstruction.Contains(':'))
                {
                    var label = trimmedInstruction.Split(':').First();
                    Globals.labelDictionary.Add(label, result.Count);
                    var textAfterLabel = trimmedInstruction.Split(':')[1];
                    if (textAfterLabel.Length > 0)
                    {
                        trimmedInstruction = textAfterLabel;
                    }
                    else
                    {
                        continue;
                    }
                }
                var instruction = InstructionFactory.GetInstruction(trimmedInstruction);
                if (instruction == null)
                {
                    MessageBox.Show($"Invalid instruction: {instructionText}", "Syntax error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                if (instruction is JumpInstruction jmpInstruction)
                {
                    instructionToChange.Add(new Tuple<int, JumpInstruction>(result.Count, jmpInstruction));
                }
                var binaryFormList = instruction.GenerateBinaryForm();
                result.AddRange(binaryFormList);
            }

            foreach (var instruction in instructionToChange)
            {
                var binaryForm = instruction.Item2.GenerateBinaryForm(instruction.Item1 + 1);
                result[instruction.Item1] = binaryForm;
            }

            return result;
        }
    }
}
