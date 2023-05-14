using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CPUSimulator.Globals;

namespace CPUSimulator
{
    internal class Simulator
    {
        public Simulator()
        {
            var microprogram = File.ReadAllLines("Codificare.txt");
            for (int i = 0; i < microprogram.Length; i++)
            {
                MPM[i] = Convert.ToInt64(microprogram[i], 2);
            }
        }

        public void LoadProgram(List<int> program, short offset)
        {
            for (int i = 0; i < program.Count; i++)
            {
                MPM[i + offset] = Convert.ToInt64(program[i]);
            }
            PC = offset;
        }


    }

}
