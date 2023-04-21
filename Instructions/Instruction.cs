using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUSimulator.Instructions
{
    internal abstract class Instruction
    {
        public Instruction(string text)
        {
            TextForm = text;
        } 
        public string TextForm { get; }

        public abstract List<int> GenerateBinaryForm();
    }
}
