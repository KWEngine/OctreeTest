using OctreeTest;
using OpenTK.Windowing.Desktop;

GameWindowSettings gws = GameWindowSettings.Default;

NativeWindowSettings nws = new NativeWindowSettings();
nws.Flags = OpenTK.Windowing.Common.ContextFlags.Debug;
nws.NumberOfSamples = 0; // FSAA
nws.Title = "Mein OpenGL-Projekt";
nws.WindowBorder = OpenTK.Windowing.Common.WindowBorder.Resizable;

Window w = new Window(gws, nws);
w.Run();
w.Dispose();
