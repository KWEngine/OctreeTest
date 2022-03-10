using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctreeTest
{
    class Node
    {
        public float Length { get; private set; } = 25;
        public Vector3 Center { get; private set; } = Vector3.Zero;

        public List<Node> Children { get; private set; } = new List<Node>();
    }
}
