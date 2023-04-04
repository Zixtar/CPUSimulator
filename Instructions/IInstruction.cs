using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUSimulator.Instructions
{
    internal interface IInstruction
    {
        string TextForm { get; }
        int BinaryForm { get; }
        int GetBinaryForm();

        int GenerateBinaryForm();
    }
}
