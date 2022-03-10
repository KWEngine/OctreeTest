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
        public Vector3 Scale { get; private set; } = new Vector3(1, 1, 1);
        public Matrix4 ModelMatrix = Matrix4.Identity;

        public Hitbox(Vector3 p)
        {
            ModelMatrix = Matrix4.CreateTranslation(p);
        }
    }
}
