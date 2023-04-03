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
        public List<string> ParseInstructionList(List<string> instructionList)
        {
            List<string> result = new List<string>();   
            foreach(var instructionText in instructionList)
            {
                var trimmedInstruction= instructionText.ReplaceLineEndings().Trim().Split(";").First();
                if (string.IsNullOrEmpty(trimmedInstruction)) continue;
                var instruction = InstructionFactory.GetInstuction(trimmedInstruction);
                if (instruction == null)
                {
                    MessageBox.Show($"Invalid instruction: {instructionText}", "Sintax error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                //var binaryFrom = instruction.GetBinaryForm();
                if(trimmedInstruction.Contains(':') && trimmedInstruction.Last()!=':')
                {
                    result.Add(trimmedInstruction.Split(':').First() + ":");
                    result.Add(trimmedInstruction.Split(':')[1].Trim(' '));
                }
                else 
                {
                    result.Add(trimmedInstruction);
                }
                
            }
            return result;
        }
    }
}
