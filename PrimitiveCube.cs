using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace OctreeTest
{
    static class PrimitiveCube
    {
        public static int VertexCount { get; private set; } = 36;
        public static int VAO { get; private set; } = -1;
        private static int VBO { get; set; }

        private static float[] VERTICES = {
       
            // front face
            -0.5f, 0.5f, 0.5f, 
            -0.5f, -0.5f, 0.5f,
            0.5f, 0.5f, 0.5f, 
            -0.5f, -0.5f, 0.5f,
            0.5f, -0.5f, 0.5f,
            0.5f, 0.5f, 0.5f,
            // right face
            0.5f, 0.5f, 0.5f,
            0.5f, -0.5f, 0.5f,
            0.5f, 0.5f, -0.5f,
            0.5f, -0.5f, 0.5f,
            0.5f, -0.5f, -0.5f,
            0.5f, 0.5f, -0.5f,
            // back face
            0.5f, 0.5f, -0.5f,
            0.5f, -0.5f, -0.5f,
            -0.5f, 0.5f, -0.5f,
            0.5f, -0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f,
            -0.5f, 0.5f, -0.5f,
            // left face
            -0.5f, 0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f,
            -0.5f, 0.5f, 0.5f,
            -0.5f, -0.5f, -0.5f,
            -0.5f, -0.5f, 0.5f,
            -0.5f, 0.5f, 0.5f,
            // top face
            -0.5f, 0.5f, -0.5f,
            -0.5f, 0.5f, 0.5f,
            0.5f, 0.5f, -0.5f,
            -0.5f, 0.5f, 0.5f,
            0.5f, 0.5f, 0.5f,
            0.5f, 0.5f, -0.5f,
            // bottom face
            0.5f, -0.5f, -0.5f,
            0.5f, -0.5f, 0.5f,
            -0.5f, -0.5f, -0.5f,
            0.5f, -0.5f, 0.5f,
            -0.5f, -0.5f, 0.5f,
            -0.5f, -0.5f, -0.5f
        };

        public static void Init()
        {
            if (VAO < 0)
            {
                VAO = GL.GenVertexArray();

                // GeoCube positions for shadow mapping and skybox:
                GL.BindVertexArray(VAO);
                VBO = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
                GL.BufferData(BufferTarget.ArrayBuffer, VERTICES.Length * 4, VERTICES, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.EnableVertexAttribArray(0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.BindVertexArray(0);
            }
            else
            {
                throw new Exception("init already done.");
            }
        }
    }
}
