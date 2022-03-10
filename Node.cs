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
        private static readonly Vector3[] COLORS = new Vector3[] {
            new Vector3(0, 0, 1),
            new Vector3(1, 0, 0),
            new Vector3(1, 1, 0),
            new Vector3(0, 1, 0),
            new Vector3(1, 0, 1),
            new Vector3(0, 0, 0),
            new Vector3(0, 1, 1),
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(0.25f, 0.75f, 0.5f)
        };
        private static readonly int MaxHitboxes = 4;
        private static int Counter = 0;
        public Vector3 Scale { get; private set; } = new Vector3(1, 1, 1);
        public Vector3 Center { get; private set; } = Vector3.Zero;
        public Vector3 Color { get; private set; } = COLORS[Counter % COLORS.Length];

        public List<Hitbox> Hitboxes = new List<Hitbox>();

        public List<Node> ChildNodes { get; private set; } = new List<Node>();

        public Node(Vector3 scale, Vector3 center)
        {
            Counter++;
            Scale = scale;
            Center = center;
        }

        public bool AddHitbox(Hitbox h)
        {
            if(Hitboxes.Count == MaxHitboxes && ChildNodes.Count == 0)
            {
                Subdivide();
                bool result;
                foreach (Node n in ChildNodes)
                {
                    result = n.AddHitbox(h);
                    if (result)
                        return true;
                }
                throw new Exception("Could not place hitbox center into a node.");
            }
            else
            {
                if(Hitboxes.Count < MaxHitboxes)
                {

                }
                float top = Center.Y + Scale.Y;
                float bottom = Center.Y - Scale.Y;
                float left = Center.X - Scale.X;
                float right = Center.X + Scale.X;
                float front = Center.Z + Scale.Z;
                float back = Center.Z - Scale.Z;

                Vector3 hitboxCenter = h.ModelMatrix.Row3.Xyz;
                if (hitboxCenter.X >= left && hitboxCenter.X <= right
                    && hitboxCenter.Y >= bottom && hitboxCenter.Y <= top
                    && hitboxCenter.Z >= back && hitboxCenter.Z <= front)
                {
                    Hitboxes.Add(h);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public void Subdivide()
        {
            Node child01 = new Node(Scale / 2, Center + new Vector3(Scale.X / 2, Scale.Y / 2, Scale.Z / 2));
            ChildNodes.Add(child01);

            Node child02 = new Node(Scale / 2, Center + new Vector3(-Scale.X / 2, Scale.Y / 2, Scale.Z / 2));
            ChildNodes.Add(child02);

            Node child03 = new Node(Scale / 2, Center + new Vector3(Scale.X / 2, -Scale.Y / 2, Scale.Z / 2));
            ChildNodes.Add(child03);

            Node child04 = new Node(Scale / 2, Center + new Vector3(-Scale.X / 2, -Scale.Y / 2, Scale.Z / 2));
            ChildNodes.Add(child04);
            
            Node child05 = new Node(Scale / 2, Center + new Vector3(Scale.X / 2, Scale.Y / 2, -Scale.Z / 2));
            ChildNodes.Add(child05);

            Node child06 = new Node(Scale / 2, Center + new Vector3(-Scale.X / 2, Scale.Y / 2, -Scale.Z / 2));
            ChildNodes.Add(child06);

            Node child07 = new Node(Scale / 2, Center + new Vector3(Scale.X / 2, -Scale.Y / 2, -Scale.Z / 2));
            ChildNodes.Add(child07);

            Node child08 = new Node(Scale / 2, Center + new Vector3(-Scale.X / 2, -Scale.Y / 2, -Scale.Z / 2));
            ChildNodes.Add(child08);
            
        }
    }
}
