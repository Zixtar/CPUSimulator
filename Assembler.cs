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
        Dictionary<string,int> labelDictionary=new Dictionary<string,int>();
        public List<int> ParseInstructionList(List<string> instructionList)
        {
            List<int> result = new List<int>();   
            foreach(var instructionText in instructionList)
            {
                var trimmedInstruction= instructionText.ReplaceLineEndings().Trim().Split(";").First();
                if (string.IsNullOrEmpty(trimmedInstruction)) continue;
                if (trimmedInstruction.Contains(':'))
                {
                    var label = trimmedInstruction.Split(':').First();
                    labelDictionary.Add(label, result.Count);
                    var textAfterLabel = trimmedInstruction.Split(':')[1];
                    if(textAfterLabel.Length > 0) 
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
                var binaryFormList = instruction.GenerateBinaryForm();
                result.AddRange(binaryFormList);
                
            }
            return result;
        }
    }
}
