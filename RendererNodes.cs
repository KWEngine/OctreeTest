using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OctreeTest
{
    static class RendererNodes
    {
        private static float[] _dummy = new float[] {0f, 0f, 0f};
        private static int VAO = -1;
        private static int VBO = -1;

        public static int ProgramID { get; private set; } = -1;

        public static int UViewProjectionMatrix { get; private set; } = -1;
        public static int UCenter { get; private set; } = -1;
        public static int URadius { get; private set; } = -1;

        private static int LoadShader(Stream pFileStream, ShaderType pType, int pProgram)
        {
            int address = GL.CreateShader(pType);
            using (StreamReader sr = new StreamReader(pFileStream))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(pProgram, address);
            return address;
        }

        public static void Init()
        {
            if(ProgramID < 0)
            {
                VAO = GL.GenVertexArray();
                GL.BindVertexArray(VAO);
                VBO = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
                GL.BufferData(BufferTarget.ArrayBuffer, _dummy.Length * 4, _dummy, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.EnableVertexAttribArray(0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.BindVertexArray(0);

                ProgramID = GL.CreateProgram();

                string resourceNameFragmentShader = "OctreeTest.renderernodes.frag";
                string resourceNameGeometryShader = "OctreeTest.renderernodes.geom";
                string resourceNameVertexShader = "OctreeTest.renderernodes.vert";
                
                int vertexShader;
                int geometryShader;
                int fragmentShader;
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (Stream s = assembly.GetManifestResourceStream(resourceNameVertexShader))
                {
                    vertexShader = LoadShader(s, ShaderType.VertexShader, ProgramID);
                }

                using (Stream s = assembly.GetManifestResourceStream(resourceNameGeometryShader))
                {
                    geometryShader = LoadShader(s, ShaderType.GeometryShader, ProgramID);
                }

                using (Stream s = assembly.GetManifestResourceStream(resourceNameFragmentShader))
                {
                    fragmentShader = LoadShader(s, ShaderType.FragmentShader, ProgramID);
                }

                if (vertexShader >= 0 && geometryShader >= 0 && fragmentShader >= 0)
                {
                    GL.LinkProgram(ProgramID);
                }
                else
                {
                    throw new Exception("Creating and linking shaders failed.");
                }

                UCenter = GL.GetUniformLocation(ProgramID, "uCenter");
                URadius = GL.GetUniformLocation(ProgramID, "uRadius");
                UViewProjectionMatrix = GL.GetUniformLocation(ProgramID, "uViewProjectionMatrix");
            }
        }

        public static void Draw(Node n, ref Matrix4 vp)
        {
            GL.Uniform3(UCenter, n.Center);
            GL.Uniform1(URadius, n.Length / 2f);
            GL.BindVertexArray(VAO);
            GL.DrawArrays(PrimitiveType.Points, 0, 1); 
            GL.BindVertexArray(0);
            Helper.CheckGLErrors();

            foreach(Node child in n.Children)
            {
                Draw(n, ref vp);
            }
        }
    }
}
