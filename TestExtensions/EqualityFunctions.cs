using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestExtensions
{
    /// <summary>
    /// A collection of useful equality functions for unit tests
    /// </summary>
    public static class EqualityFunctions
    {
        /// <summary>
        /// A custom equality function we use to avoid performing reference equality on Points.
        /// Checks the two points are equal to within 0.00001
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool PointEquality(Point lhs, Point rhs)
        {
            double epsilon = 0.00001;
            return Math.Abs(lhs.X - rhs.X) < epsilon &&
                   Math.Abs(lhs.Y - rhs.Y) < epsilon;
        }

        /// <summary>
        /// Extra function we use in file finding to avoid using straight string comparison on files.
        /// That does not take into account ..\ for example.
        /// What we are really checking for is whether the paths point to the same location, which is why we use Path.GetFullPath.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool PathEquality(string lhs, string rhs)
        {
            // Either the strings are null/empty or they should be valid paths
            // If they are not valid paths, then that is probably another error, but should not be ignored
            return (string.IsNullOrEmpty(lhs) && string.IsNullOrEmpty(rhs)) || (Path.GetFullPath(lhs) == Path.GetFullPath(rhs));
        }
    }
}
