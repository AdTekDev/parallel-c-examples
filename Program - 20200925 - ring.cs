using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPI;

namespace S02_SS_MPI
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = -1;

            MPI.Environment.Run(ref args, comm =>
            {
                if (comm.Rank == 0)
                {
                    x = 100;
                    comm.Send(x, 1, 111);

                    comm.Receive(comm.Size - 1, 111, out x);
                    Console.WriteLine("Master 0 receive Data= " + x);

                }
                else
                {
                    comm.Receive(comm.Rank - 1, 111, out x);
                    int y = x + comm.Rank;
                    comm.Send(y, (comm.Rank + 1) % comm.Size, 111);

                    Console.WriteLine("SLAVE " + comm.Rank
                        + " / " + comm.Size + " receive DATA= " + x
                        + " on " + MPI.Environment.ProcessorName
                        + " / " + Communicator.world.Size + " processes "
                        );
                }
            });
        }
    }
}
