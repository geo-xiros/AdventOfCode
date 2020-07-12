using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace AdventOfCode
{

    public class ArcadeCabinet
    {
        private IntComputer computer = new IntComputer();
        private Dictionary<Point, long> screen = new Dictionary<Point, long>();
        private long paddleX;
        private long joystickInput;
        protected long x;
        protected long y;

        public long Blocks => screen.Where(s => s.Value == 2).Count();
        public long Score { get; set; }

        public ArcadeCabinet()
        {
        }

        public void Run()
        {

            computer.LoadProgram(FileUtils.LoadDataFor(13))
                .SetInput(() => joystickInput)
                .SetOutput(GetX)
                .Run();
        }

        public void Run(long quarters)
        {

            computer.LoadProgram(FileUtils.LoadDataFor(13))
                .SetInput(()=> joystickInput)
                .SetOutput(GetX)
                .SetMemory(quarters)
                .Run();
        }

        private void GetX(long output)
        {
            x = output;
            computer.SetOutput(GetY);
        }

        private void GetY(long output)
        {
            y = output;
            computer.SetOutput(GetTile);
        }

        private void GetTile(long output)
        {
            if (x == -1 && y == 0)
            {
                Score = output;
                RenderScore();
            }
            else
            {
                screen[new Point((int)x, (int)y)] = output;
                Render(output);

                if (output == 4)
                {
                    if (x > paddleX)
                    {
                        joystickInput = 1;
                    }
                    else if (x < paddleX)
                    {
                        joystickInput = -1;
                    }
                }
                else if (output == 3)
                {
                    paddleX = x;
                }
                else
                {
                    joystickInput = 0;
                }
            }

            computer.SetOutput(GetX);
        }

        protected virtual void Render(long output) { }
        protected virtual void RenderScore() { }

        private IEnumerator<long> JoystickInput()
        {
            while (true)
            {
                yield return joystickInput;
            }
        }
    }
}