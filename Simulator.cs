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
        public Seq Sequencer;

        public Simulator(List<int> program)
        {
            for (int i = 0; i < program.Count; i++)
            {
                MEM[i] = Convert.ToUInt16(program[i]);
            }
            PC = -1;
            var microprogram = File.ReadAllLines(@"The Holy Grail\Codificare.txt");
            for (int i = 0; i < microprogram.Length; i++)
            {
                MPM[i] = Convert.ToInt64(microprogram[i], 2);
            }
            MAR = 0;
            BPO = true;
            SP = (short)(MEM.Length - 1);
        }

        public void Start()
        {
            Sequencer = new Seq();
            Sequencer.StartSeq();
            ResetColors();
        }

        public void DoLoop()
        {
            if (BPO)
            {
                ResetColors();
                Sequencer.DoCore();
            }
        }

    }

}
