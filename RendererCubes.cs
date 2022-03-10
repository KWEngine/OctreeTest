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
    static class RendererCubes
    {
        public static int ProgramID { get; private set; } = -1;
        public static int UModelMatrix { get; private set; } = -1;
        public static int UViewProjectionMatrix { get; private set; } = -1;

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
                ProgramID = GL.CreateProgram();

                string resourceNameFragmentShader = "OctreeTest.renderercubes.frag";
                string resourceNameVertexShader = "OctreeTest.renderercubes.vert";
                
                int vertexShader;
                int fragmentShader;
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (Stream s = assembly.GetManifestResourceStream(resourceNameVertexShader))
                {
                    vertexShader = LoadShader(s, ShaderType.VertexShader, ProgramID);
                }

                using (Stream s = assembly.GetManifestResourceStream(resourceNameFragmentShader))
                {
                    fragmentShader = LoadShader(s, ShaderType.FragmentShader, ProgramID);
                }

                if (vertexShader >= 0 && fragmentShader >= 0)
                {
                    GL.LinkProgram(ProgramID);
                }
                else
                {
                    throw new Exception("Creating and linking shaders failed.");
                }

                UModelMatrix = GL.GetUniformLocation(ProgramID, "uModelMatrix");
                UViewProjectionMatrix = GL.GetUniformLocation(ProgramID, "uViewProjectionMatrix");
            }
        }

        public static void Draw(Hitbox h, ref Matrix4 vp)
        {
            Helper.CheckGLErrors();
            GL.UniformMatrix4(UModelMatrix, false, ref h.ModelMatrix);
            GL.UniformMatrix4(UViewProjectionMatrix, false, ref vp);
            Helper.CheckGLErrors();
            GL.BindVertexArray(PrimitiveCube.VAO);
            GL.DrawArrays(PrimitiveType.Triangles, 0, PrimitiveCube.VertexCount);
            GL.BindVertexArray(0);
            Helper.CheckGLErrors();
        }
    }
}
