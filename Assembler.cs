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
            List<Tuple<int, string>> instructionToChange = new();
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
                if (instruction is JumpInstruction jmpInstruction && ((jmpInstruction.GenerateBinaryForm().First() & 0x30) == 0))
                {
                    instructionToChange.Add(new Tuple<int, string>(result.Count + 1, 
                        jmpInstruction.TextForm.Split(" ".ToCharArray())[1]));
                }
                var binaryFormList = instruction.GenerateBinaryForm();
                result.AddRange(binaryFormList);
            }

            foreach (var instruction in instructionToChange)
            {
                var offset = Globals.labelDictionary[instruction.Item2] - instruction.Item1-1;
                if(offset < 0)
                {
                    offset = Int16.MaxValue + offset + 1;
                }
                result[instruction.Item1] = offset;
            }

            return result;
        }
    }
}
