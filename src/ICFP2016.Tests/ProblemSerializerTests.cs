using ICFP2016.Core.Services;
using NUnit.Framework;

namespace ICFP2016.Tests
{
    [TestFixture]
    public class ProblemSerializerTests
    {
        [Test]
        public void TestLoadProblem()
        {
            var ps = new ProblemSerializer();

            var problem = ps.LoadProblem(@"c:\users\tstivers\source\repos\icfp2016\problems\sample1.txt");

            Assert.That(problem.Silhouette.Polygons.Count, Is.EqualTo(1));
            Assert.That(problem.Silhouette.Polygons[0].Vertices.Count, Is.EqualTo(4));
            Assert.That(problem.Silhouette.Polygons[0].Vertices[0].X.A, Is.EqualTo(0));
            Assert.That(problem.Silhouette.Polygons[0].Vertices[0].X.B, Is.EqualTo(1));
            Assert.That(problem.Silhouette.Polygons[0].Vertices[1].X.A, Is.EqualTo(1));
            Assert.That(problem.Silhouette.Polygons[0].Vertices[1].X.B, Is.EqualTo(1));
            Assert.That(problem.Skeleton.Segments.Count, Is.EqualTo(5));
            Assert.That(problem.Skeleton.Segments[0].A.X.A, Is.EqualTo(0));
            Assert.That(problem.Skeleton.Segments[0].B.X.A, Is.EqualTo(1));
        }
    }
}