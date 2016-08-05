using ICFP2016.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ICFP2016.Core.Services
{
    public class FileContents
    {
        private readonly string[] _lines;
        private int _current;

        public FileContents(string[] lines)
        {
            _lines = lines;
        }

        public int ReadCount()
        {
            return Int32.Parse(_lines[_current++]);
        }

        public Vertex ReadVertex()
        {
            var v = new Vertex { X = new Coordinate(), Y = new Coordinate() };

            var match = Regex.Match(_lines[_current++], @"(-?\d+)(?:/(\d+))?,(-?\d+)(?:/(\d+))?");

            if (!match.Success)
                throw new InvalidOperationException();

            v.X.A = Convert.ToInt32(match.Groups[1].Value);
            v.X.B = Convert.ToInt32(string.IsNullOrEmpty(match.Groups[2].Value) ? "1" : match.Groups[2].Value);
            v.Y.A = Convert.ToInt32(match.Groups[3].Value);
            v.Y.B = Convert.ToInt32(string.IsNullOrEmpty(match.Groups[4].Value) ? "1" : match.Groups[4].Value);

            return v;
        }

        public Segment ReadSegment()
        {
            var a = new Vertex { X = new Coordinate(), Y = new Coordinate() };
            var b = new Vertex { X = new Coordinate(), Y = new Coordinate() };
            var s = new Segment
            {
                A = a,
                B = b
            };

            var matches = Regex.Match(_lines[_current++], @"(-?\d+)(?:/(\d+))?,(-?\d+)(?:/(\d+))? (-?\d+)(?:/(\d+))?,(-?\d+)(?:/(\d+))?");

            if (!matches.Success)
                throw new InvalidOperationException();

            a.X.A = Convert.ToInt32(matches.Groups[1].Value);
            a.X.B = Convert.ToInt32(string.IsNullOrEmpty(matches.Groups[2].Value) ? "1" : matches.Groups[2].Value);
            a.Y.A = Convert.ToInt32(matches.Groups[3].Value);
            a.Y.B = Convert.ToInt32(string.IsNullOrEmpty(matches.Groups[4].Value) ? "1" : matches.Groups[4].Value);

            b.X.A = Convert.ToInt32(matches.Groups[5].Value);
            b.X.B = Convert.ToInt32(string.IsNullOrEmpty(matches.Groups[6].Value) ? "1" : matches.Groups[6].Value);
            b.Y.A = Convert.ToInt32(matches.Groups[7].Value);
            b.Y.B = Convert.ToInt32(string.IsNullOrEmpty(matches.Groups[8].Value) ? "1" : matches.Groups[8].Value);

            return s;
        }
    }

    public class ProblemSerializer
    {
        public Problem LoadProblem(string filename)
        {
            var p = new Problem();
            var contents = new FileContents(File.ReadAllLines(filename));

            p.Silhouette = new Silhouette();
            p.Silhouette.Polygons = new List<Polygon>();
            var polygons = contents.ReadCount();

            for (int i = 0; i < polygons; i++)
            {
                var polygon = new Polygon();
                polygon.Vertices = new List<Vertex>();

                var vertices = contents.ReadCount();
                for (int j = 0; j < vertices; j++)
                {
                    polygon.Vertices.Add(contents.ReadVertex());
                }
                p.Silhouette.Polygons.Add(polygon);
            }

            p.Skeleton = new Skeleton();
            p.Skeleton.Segments = new List<Segment>();

            var segments = contents.ReadCount();
            for (int i = 0; i < segments; i++)
            {
                p.Skeleton.Segments.Add(contents.ReadSegment());
            }

            return p;
        }

        public void SaveProblem(Problem problem, string filename)
        {
            throw new NotImplementedException();
        }
    }
}