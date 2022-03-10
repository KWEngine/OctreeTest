using OctreeTest;
using OpenTK.Windowing.Desktop;

GameWindowSettings gws = GameWindowSettings.Default;

NativeWindowSettings nws = new NativeWindowSettings();
nws.Flags = OpenTK.Windowing.Common.ContextFlags.Debug;
nws.NumberOfSamples = 8;
nws.Title = "Mein OpenGL-Projekt";
nws.WindowBorder = OpenTK.Windowing.Common.WindowBorder.Resizable;

Window w = new Window(gws, nws);
w.VSync = OpenTK.Windowing.Common.VSyncMode.On;
w.Run();
w.Dispose();
