using System.Text;

using Solutions;

namespace Advent2022
{
    internal class Day10 : Solution<int>
    {
        public Day10() : base(10) { }

        protected override int partOne()
        {
            int reg = 1;
            int clk = 1;
            int sig = 0;

            int op = 0;
            for (int ln = 0; ln < input.data.Length;)
            {
                if ((clk - 20) % 40 == 0)
                    sig += clk * reg;

                var instr = input.data[ln].Split(' ');
                if (instr[0].Equals("addx"))
                {
                    if (op == 0)
                        op = int.Parse(instr[1]);
                    else
                    {
                        reg += op;
                        op = 0;
                        ln++;
                    }
                    clk++;
                }
                else
                {
                    ln++;
                    clk++;
                }
            }
            return sig;
        }


        protected override int partTwo()
        {
            var message = new StringBuilder();
            int reg = 1;
            int clk = 1;

            int op = 0;
            for (int ln = 0; ln < input.data.Length;)
            {
                int spritePos = (clk - 1) % 40;
                if (spritePos == 0)
                    message.Append("\n");

                message.Append(Math.Abs(spritePos - reg) <= 1 ? "#" : " ");

                var instr = input.example[ln].Split(' ');
                if (instr[0].Equals("addx"))
                {
                    if (op == 0)
                        op = int.Parse(instr[1]);
                    else
                    {
                        reg += op;
                        op = 0;
                        ln++;
                    }
                    clk++;
                }
                else
                {
                    ln++;
                    clk++;
                }
            }
            Console.WriteLine(message.ToString());
            return -1;
        }
    }
}