using System;

namespace AdventOfCode
{
    public class PanelPosition : IEquatable<PanelPosition>
    {
        private string direction;

        public long X { get; }
        public long Y { get; }

        public PanelPosition(long x, long y, string direction)
        {
            X = x;
            Y = y;
            this.direction = direction;
        }

        internal PanelPosition MoveLeft()
        {
            var newDrirection = GetNewDirection(true);
            return MoveTo(newDrirection);
        }

        internal PanelPosition MoveRight()
        {
            var newDrirection = GetNewDirection(false);
            return MoveTo(newDrirection);
        }

        private PanelPosition MoveTo(string newDrirection)
        {
            switch (newDrirection)
            {
                case "^":
                    return new PanelPosition(X - 1, Y, newDrirection);
                case "<":
                    return new PanelPosition(X, Y - 1, newDrirection);
                case "v":
                    return new PanelPosition(X + 1, Y, newDrirection);
                case ">":
                    return new PanelPosition(X, Y + 1, newDrirection);
            }

            return this;
        }

        private string GetNewDirection(bool rotateLeft)
        {
            switch (direction)
            {
                case "^":
                    return rotateLeft ? "<" : ">";
                case "<":
                    return rotateLeft ? "v" : "^";
                case "v":
                    return rotateLeft ? ">" : "<";
                case ">":
                    return rotateLeft ? "^" : "v";
            }

            return direction;
        }

        public bool Equals(PanelPosition other)
        {
            return other != null && (other.X, other.Y) == (X, Y);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj as PanelPosition);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }
}