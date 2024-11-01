using Figures;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GameTest
{
    public class Game : GameWindow
    {
        private Figure[] figures;

        public Game(int width, int height, string title)
            : base(
                GameWindowSettings.Default,
                new NativeWindowSettings() { ClientSize = (width, height), Title = title }
            )
        {
            this.figures = new Figure[]
            {
                // Center triangle pointing up
                new Triangle(
                    new float[] { -0.2f, -0.2f, 0.0f, 0.2f, -0.2f, 0.0f, 0.0f, 0.2f, 0.0f }
                ),
                // Center triangle pointing down
                new Triangle(
                    new float[] { -0.2f, 0.2f, 0.0f, 0.2f, 0.2f, 0.0f, 0.0f, -0.2f, 0.0f }
                ),
                // Left triangle
                new Triangle(
                    new float[] { -0.6f, 0.0f, 0.0f, -0.2f, 0.2f, 0.0f, -0.2f, -0.2f, 0.0f }
                ),
                // Right triangle
                new Triangle(new float[] { 0.6f, 0.0f, 0.0f, 0.2f, 0.2f, 0.0f, 0.2f, -0.2f, 0.0f }),
                // Top triangle
                new Triangle(new float[] { 0.0f, 0.6f, 0.0f, -0.2f, 0.2f, 0.0f, 0.2f, 0.2f, 0.0f }),
                // Bottom triangle
                new Triangle(
                    new float[] { 0.0f, -0.6f, 0.0f, -0.2f, -0.2f, 0.0f, 0.2f, -0.2f, 0.0f }
                ),
                // Top-left diagonal triangle
                new Triangle(
                    new float[] { -0.4f, 0.4f, 0.0f, -0.2f, 0.2f, 0.0f, -0.1f, 0.5f, 0.0f }
                ),
                // Top-right diagonal triangle
                new Triangle(new float[] { 0.4f, 0.4f, 0.0f, 0.2f, 0.2f, 0.0f, 0.1f, 0.5f, 0.0f }),
                // Bottom-left diagonal triangle
                new Triangle(
                    new float[] { -0.4f, -0.4f, 0.0f, -0.2f, -0.2f, 0.0f, -0.1f, -0.5f, 0.0f }
                ),
                // Bottom-right diagonal triangle
                new Triangle(
                    new float[] { 0.4f, -0.4f, 0.0f, 0.2f, -0.2f, 0.0f, 0.1f, -0.5f, 0.0f }
                ),
            };
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnLoad()
        {
            foreach (var figure in this.figures)
            {
                figure.Load();
            }
            base.OnLoad();
        }

        protected override void OnUnload()
        {
            foreach (var figure in this.figures)
            {
                figure.Unload();
            }
            base.OnUnload();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            foreach (var figure in this.figures)
            {
                figure.Render();
            }
            SwapBuffers();
            base.OnRenderFrame(args);
        }
    }
}
