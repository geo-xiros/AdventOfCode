using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode
{
    public class PaintingRobot
    {
        private IntComputer computer = new IntComputer();
        private Dictionary<PanelPosition, long> panelsGridPaintStatus = new Dictionary<PanelPosition, long>();
        private PanelPosition currentPosition = new PanelPosition(0, 0, "^");

        public long PanelsPaintedAtLeastOnce => panelsGridPaintStatus.Keys.Count();

        public PaintingRobot(long startingPointColor = 0)
        {
            panelsGridPaintStatus.Add(currentPosition, startingPointColor);

            computer.LoadProgram(FileUtils.LoadDataFor(11))
                .SetInput(ColorInput)
                .SetOutput(Paint)
                .Run();
        }

        public void PrintRegistrationIdentifier()
        {
            var maxY = (int)panelsGridPaintStatus.Keys.Select(p => p.Y).Max();
            var maxX = (int)panelsGridPaintStatus.Keys.Select(p => p.X).Max();

            foreach (var p in panelsGridPaintStatus)
            {
                Console.SetCursorPosition((int)p.Key.Y + maxY, (int)p.Key.X + maxX);
                Console.Write(p.Value == 1 ? "#" : "");
            }

            Console.SetCursorPosition(0, 0);
        }

        private void Paint(long output)
        {
            panelsGridPaintStatus[currentPosition] = output;
            computer.SetOutput(Move);
        }

        private void Move(long output)
        {
            switch (output)
            {
                case 0:
                    currentPosition = currentPosition.MoveLeft();
                    break;
                case 1:
                    currentPosition = currentPosition.MoveRight();
                    break;
                default:
                    break;
            }

            computer.SetOutput(Paint);
        }

        private long ColorInput()
        {
            return (panelsGridPaintStatus.TryGetValue(currentPosition, out long painted))
                ? painted
                : 0;
        }
    }
}