using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctreeTest
{
    class Hitbox
    {
        private static readonly Vector3[] BASEVERTICES = new Vector3[]
        {
            new Vector3(-0.5f, -0.5f, -0.5f), // lower left  back
            new Vector3(+0.5f, -0.5f, -0.5f), // lower right back
            new Vector3(-0.5f, -0.5f, +0.5f), // lower left  front
            new Vector3(+0.5f, -0.5f, +0.5f), // lower right front
            new Vector3(-0.5f, +0.5f, -0.5f), // upper left  back
            new Vector3(+0.5f, +0.5f, -0.5f), // upper right back
            new Vector3(-0.5f, +0.5f, +0.5f), // upper left  front
            new Vector3(+0.5f, +0.5f, +0.5f)  // upper right front
        };

        private static readonly Vector3 BASECENTER = new Vector3(0, 0, 0);
        private Vector3[] _vertices = new Vector3[8];

        public Vector3 Scale { get; private set; } = new Vector3(1, 1, 1);
        public Matrix4 ModelMatrix = Matrix4.Identity;

        public Hitbox(Vector3 p, Vector3 scale)
        {
            ModelMatrix = Matrix4.CreateScale(scale) * Matrix4.CreateTranslation(p);
        }

        public void Update()
        {
            for(int i = 0; i < BASEVERTICES.Length; i++)
            {
                _vertices[i] = Vector3.TransformPosition(BASEVERTICES[i], ModelMatrix);
            }
        }
    }
}
