using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUSimulator
{
    public class Assembler
    {
        public List<string> ParseInstructionList(List<string> instructionList)
        {
            List<string> result = new List<string>();   
            foreach(var instruction in instructionList)
            {
                if (instruction == "") continue;
                var trimmedInstruction=instruction.ReplaceLineEndings().Trim('\t').Split(";").First();
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
