namespace AdventOfCode
{
    public class Day13
    {
        private long _answer1;
        private long _answer2;

        public Day13()
        {

            var arcadeCabinet = new ArcadeCabinet();

            arcadeCabinet.Run();
            _answer1 = arcadeCabinet.Blocks;

            arcadeCabinet.Run(2);
            _answer2 = arcadeCabinet.Score;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name} => Answer A: {_answer1}, Answer B: {_answer2}";
        }
    }
}