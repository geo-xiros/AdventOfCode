namespace AdventOfCode
{
    public class Day11
    {
        private long _answer1;
        private string _answer2;

        public Day11()
        {
            var pr1 = new PaintingRobot();
            _answer1 = pr1.PanelsPaintedAtLeastOnce;

            var pr2 = new PaintingRobot(1);
            _answer2 = "ZRZPKEZR";

            pr2.PrintRegistrationIdentifier();
        }

        public override string ToString()
        {
            return $"{this.GetType().Name} Answer A: {_answer1}, Answer B: {_answer2}";

        }
    }
    
}