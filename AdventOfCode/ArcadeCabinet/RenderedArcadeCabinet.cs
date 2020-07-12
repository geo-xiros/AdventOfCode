using System;

namespace AdventOfCode
{
    public class RenderedArcadeCabinet : ArcadeCabinet
    {
        protected override void Render(long output)
        {
            Console.SetCursorPosition((int)x, (int)y + 1);
            Console.Write(output == 0L ? " " : output.ToString());
        }

        protected override void RenderScore()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write($"Score : {Score}");
        }
    }
}