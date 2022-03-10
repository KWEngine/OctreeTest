using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace OctreeTest
{
    class Window : GameWindow
    {
        private Matrix4 _viewMatrix = Matrix4.Identity;
        private Matrix4 _projectionMatrix = Matrix4.Identity;
        private Matrix4 _viewProjectionMatrix = Matrix4.Identity;
        private List<Hitbox> _hitboxes = new List<Hitbox>();
        private Node _rootNode = new Node();

        private float camCosX = 0;
        private float camSinY = 0;
        private float camDistance = 25;
        private readonly float camSpeedStep = 0.01f;


        protected override void OnLoad()
        {
            base.OnLoad();
            Helper.CheckGLErrors();
            PrimitiveCube.Init();
            Helper.CheckGLErrors();
            RendererCubes.Init();
            RendererNodes.Init();
            Helper.CheckGLErrors();

            GL.ClearColor(1, 1, 1, 1);
            GL.Enable(EnableCap.DepthTest);
            GL.CullFace(CullFaceMode.Back);
            GL.PointSize(8);
            GL.LineWidth(2);

            UpdateViewMatrix();
            GenerateHitboxes();
            
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
           
        }

        private void GenerateHitboxes()
        {
            _hitboxes.Add(new Hitbox());
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

        }

        private void UpdateViewMatrix()
        {
            camCosX = (camCosX + camSpeedStep) % ((float)Math.PI * 2);
            camSinY = (camSinY + camSpeedStep) % ((float)Math.PI * 2);
            _viewMatrix = Matrix4.LookAt((float)Math.Cos(camCosX) * camDistance, 25, (float)Math.Sin(camSinY) * camDistance, 0, 0, 0, 0, 1, 0);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            UpdateViewMatrix();

            base.OnRenderFrame(args);
            _viewProjectionMatrix = _viewMatrix * _projectionMatrix;

            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
            GL.UseProgram(RendererCubes.ProgramID);
            foreach (Hitbox h in _hitboxes)
            {
                RendererCubes.Draw(h, ref _viewProjectionMatrix);
            }

            GL.UseProgram(RendererNodes.ProgramID);
            GL.UniformMatrix4(RendererNodes.UViewProjectionMatrix, false, ref _viewProjectionMatrix);
            RendererNodes.Draw(_rootNode, ref _viewProjectionMatrix);

            GL.UseProgram(0);
            Helper.CheckGLErrors();
            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
            _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Size.X / (float)Size.Y, 0.1f, 1000f);
        }

        public Window(  GameWindowSettings gameWindowSettings, 
                        NativeWindowSettings nativeWindowSettings) 
            : base(gameWindowSettings, nativeWindowSettings)
        {
            Helper.CheckGLErrors();
        }
    }
}
