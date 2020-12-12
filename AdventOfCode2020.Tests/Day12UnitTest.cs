using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AdventOfCode2020.Tests
{
    [TestClass]
    public class Day12UnitTest
    {
        private ShipNavigation ship;
        private WaypointNavigation waypoint;

        [TestInitialize]
        public void Initialize()
        {
            ship = new ShipNavigation();
            waypoint = new WaypointNavigation(ship);
        }

        [TestMethod]
        public void TestPart1Example()
        {
            ship.Move('F',10);
            ship.Move('N',3);
            ship.Move('F',7);
            ship.Move('R',90);
            ship.Move('F',11);

            Assert.AreEqual(25, ship.ManhattanDistance);
        }

        [TestMethod]
        public void TestPart2Example()
        {
            waypoint.Move('F',10);
            waypoint.Move('N',3);
            waypoint.Move('F',7);
            waypoint.Move('R',90);
            waypoint.Move('F',11);

            Assert.AreEqual(286, ship.ManhattanDistance);
        }
    }
}
