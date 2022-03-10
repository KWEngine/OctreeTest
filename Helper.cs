using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctreeTest
{
    public static class Helper
    {
        public static bool CheckGLErrors()
        {
            bool hasError = false;
            ErrorCode c;
            while ((c = GL.GetError()) != ErrorCode.NoError)
            {
                hasError = true;
                Console.WriteLine(c.ToString());
            }
            return hasError;
        }
    }
}
