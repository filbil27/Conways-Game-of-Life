using System;

namespace filbil27.ConwaysGameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = GetGridSize(args);
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var game = new ConwaysGame(size);

            OutputState(ref game);

            System.Diagnostics.Debugger.Break();

            for (int i = 0; i < 500; i++)
            {
                game.Tick();
                OutputState(ref game);
                System.Threading.Thread.Sleep(500);
            }
        }

        /// <summary>
        /// Finds and gets the grid size from args
        /// If none is passed or is not a vaild int the default 25 will be used.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static int GetGridSize(string[] args)
        {
            int defaultSize = 25;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Contains("-size", StringComparison.OrdinalIgnoreCase)) 
                {
                    if (int.TryParse(args[i + 1], out int result))
                    {
                        return result;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return defaultSize;
        }

        static void OutputState(ref ConwaysGame game) => OutputState(game.CurrentGeneration, game.GridSize);
        static void OutputState(bool[,] state, int size)
        {
            Console.Clear();

            //Y is done for the maxium value so that 0,0 is in the bottom left corner
            for (int y = size - 1; y >= 0; y--)
            {
                for (int x = 0; x < size; x++)
                {
                    Console.Write(state[x, y] ? "1" : "0");
                    //Console.Write(state[x, y] ? "\u25A0" : "\u25A1");
                    //Console.Write(state[x, y] ? "\u25A0" : "_");
                }

                Console.WriteLine();
            }
        }
    }
}
