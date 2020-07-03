﻿using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode
{
    public class Day6
    {
        private long _answer1;
        private long _answer2;
        private Dictionary<string, Object> _orbitsMap;

        public Day6()
        {

            var orbits = ReadFile();
            _answer1 = GetTotalOrbits(orbits);
            // TODO:
            _answer2 = 0;

        }
        private long GetTotalOrbits(IEnumerable<string> orbits)
        {
            _orbitsMap = new Dictionary<string, Object>();

            foreach (var orbit in orbits)
            {
                var objectOrbit = orbit.Split(')');

                if (!_orbitsMap.TryGetValue(objectOrbit[0], out var centerObject))
                {
                    centerObject = new Object(objectOrbit[0]);
                    _orbitsMap.Add(objectOrbit[0], centerObject);
                }

                if (!_orbitsMap.TryGetValue(objectOrbit[1], out var orbitingObject))
                {
                    orbitingObject = new Object(objectOrbit[1], centerObject);
                    _orbitsMap.Add(objectOrbit[1], orbitingObject);
                }
                else if (orbitingObject.OrbitsAroundObject == null)
                {
                    orbitingObject.OrbitAround(centerObject);
                }
            }

            return _orbitsMap.Values.Select(o => o.TotalOrbits).Sum();
        }
        private IEnumerable<string> ReadFile()
        {
            using (var file = File.OpenRead("..\\..\\inputs\\input6.txt"))
            {
                using (var reader = new StreamReader(file))
                {
                    while (!reader.EndOfStream)
                    {
                        yield return reader.ReadLine();
                    }
                }
            }
        }
        public override string ToString()
        {
            return $"{this.GetType().Name} => Answer A:{_answer1}, Answer B:{_answer2}";
        }

        private class Object
        {
            public readonly string Name;
            public Object OrbitsAroundObject { get; private set; }

            public Object(string name)
            {
                Name = name;
            }

            public Object(string name, Object orbitsAroundObject) : this(name)
            {
                OrbitsAroundObject = orbitsAroundObject;
            }
            public void OrbitAround(Object obj)
            {
                OrbitsAroundObject = obj;
            }
            public int TotalOrbits => (OrbitsAroundObject?.TotalOrbits + 1) ?? 0;
        }
    }

}