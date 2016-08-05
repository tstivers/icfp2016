using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ICFP2016.Core.Models
{
    public class Problem
    {
        public Silhouette Silhouette { get; set; }

        public Skeleton Skeleton { get; set; }
    }
}