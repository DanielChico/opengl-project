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
        static float[] RED = new float[] { 1.0f, 0.0f, 0.0f, 1.0f };
        static float[] GREEN = new float[] { 0.0f, 1.0f, 0.0f, 1.0f };
        static float[] BLUE = new float[] { 0.0f, 0.0f, 1.0f, 1.0f };
        static float[] BLACK = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };
        private static float[][] colors = new float[][] { RED, GREEN, BLUE, BLACK };
        private int colorIndex = 0;
        private int frameColorChange = 200;
        private int frameCounter = 0;

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
                    new float[] { -0.2f, -0.2f, 0.0f, 0.2f, -0.2f, 0.0f, 0.0f, 0.2f, 0.0f },
                    colors[0]
                ),
                // Center triangle pointing down
                new Triangle(
                    new float[] { -0.2f, 0.2f, 0.0f, 0.2f, 0.2f, 0.0f, 0.0f, -0.2f, 0.0f },
                    colors[0]
                ),
                // Left triangle
                new Triangle(
                    new float[] { -0.6f, 0.0f, 0.0f, -0.2f, 0.2f, 0.0f, -0.2f, -0.2f, 0.0f },
                    colors[0]
                ),
                // Right triangle
                new Triangle(
                    new float[] { 0.6f, 0.0f, 0.0f, 0.2f, 0.2f, 0.0f, 0.2f, -0.2f, 0.0f },
                    colors[0]
                ),
                // Top triangle
                new Triangle(
                    new float[] { 0.0f, 0.6f, 0.0f, -0.2f, 0.2f, 0.0f, 0.2f, 0.2f, 0.0f },
                    colors[0]
                ),
                // Bottom triangle
                new Triangle(
                    new float[] { 0.0f, -0.6f, 0.0f, -0.2f, -0.2f, 0.0f, 0.2f, -0.2f, 0.0f },
                    colors[0]
                ),
                // Top-left diagonal triangle
                new Triangle(
                    new float[] { -0.4f, 0.4f, 0.0f, -0.2f, 0.2f, 0.0f, -0.1f, 0.5f, 0.0f },
                    colors[0]
                ),
                // Top-right diagonal triangle
                new Triangle(
                    new float[] { 0.4f, 0.4f, 0.0f, 0.2f, 0.2f, 0.0f, 0.1f, 0.5f, 0.0f },
                    colors[0]
                ),
                // Bottom-left diagonal triangle
                new Triangle(
                    new float[] { -0.4f, -0.4f, 0.0f, -0.2f, -0.2f, 0.0f, -0.1f, -0.5f, 0.0f },
                    colors[0]
                ),
                // Bottom-right diagonal triangle
                new Triangle(
                    new float[] { 0.4f, -0.4f, 0.0f, 0.2f, -0.2f, 0.0f, 0.1f, -0.5f, 0.0f },
                    colors[0]
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
            bool isColorChanged = false;
            if (this.frameCounter++ == this.frameColorChange)
            {
                isColorChanged = true;
                int nextColorIndex = this.colorIndex + 1;
                if (nextColorIndex > colors.Length - 1)
                {
                    nextColorIndex = 0;
                }
                this.colorIndex = nextColorIndex;
                this.frameCounter = 0;
            }

            foreach (var figure in this.figures)
            {
                if (isColorChanged)
                {
                    figure.Color = colors[this.colorIndex];
                }
                figure.Render();
            }
            SwapBuffers();
            base.OnRenderFrame(args);
        }
    }
}
