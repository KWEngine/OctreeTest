using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace OctreeTest
{
    class Window : GameWindow
    {
        private static readonly Vector3 _worldScale = new Vector3(50, 50, 50);

        private Matrix4 _viewMatrix = Matrix4.Identity;
        private Matrix4 _projectionMatrix = Matrix4.Identity;
        private Matrix4 _viewProjectionMatrix = Matrix4.Identity;
        private List<Hitbox> _hitboxes = new List<Hitbox>();
        private Node _rootNode = new Node(_worldScale, Vector3.Zero);
        private double _timeLastUpdated = 0;
        private double _timeElapsed = 0;

        private float camCosX = 0;
        private float camSinY = 0;
        private float camDistance = 200;
        private readonly float camSpeedStep = 0.005f;


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
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.PointSize(4);
            GL.LineWidth(4);

            UpdateViewMatrix();
            GenerateHitboxes();

            
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
           
        }

        private void GenerateHitboxes()
        {
            _hitboxes.Clear();
            Random r = new Random();
            
            for (int i = 0; i < 100; i++)
            {
                int pX = r.Next((int)-_worldScale.X , (int)_worldScale.X);
                int pY = r.Next((int)-_worldScale.X, (int)_worldScale.X);
                int pZ = r.Next((int)-_worldScale.X, (int)_worldScale.X);

                Hitbox htest01 = new Hitbox(new Vector3(pX, pY, pZ), new Vector3(2,2,2));
                _hitboxes.Add(htest01);
            }
            
            //GenerateOctree();
        }

        private void GenerateOctree()
        {
            Node.ResetColors();
            _rootNode = new Node(_worldScale, Vector3.Zero);
            foreach(Hitbox h in _hitboxes)
            {
                _rootNode.AddHitbox(h);
            }
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            _timeElapsed += args.Time;
            if(KeyboardState.IsKeyDown(Keys.F1))
            {
                if (_timeElapsed - _timeLastUpdated >= 0.5)
                {
                    GenerateHitboxes();
                    _timeLastUpdated = _timeElapsed;
                }
            }
            GenerateOctree();
        }

        private void UpdateViewMatrix()
        {
            
            camCosX = (camCosX + camSpeedStep) % ((float)Math.PI * 2);
            camSinY = (camSinY + camSpeedStep) % ((float)Math.PI * 2);
            _viewMatrix = Matrix4.LookAt((float)Math.Cos(camCosX) * camDistance, camDistance / 2, (float)Math.Sin(camSinY) * camDistance, 0, 0, 0, 0, 1, 0);
            
            //_viewMatrix = Matrix4.LookAt(0, camDistance, camDistance, 0, 0, 0, 0, 1, 0);
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
