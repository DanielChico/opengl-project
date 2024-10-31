using OpenTK.Graphics.OpenGL4;

namespace Figures
{
    public class Triangle : Figure
    {
        private float[] vertices;
        private int vertexBufferHandle;
        private int shaderProgramHandle;
        private int vertexArrayHandle;
        private string vertexPath = "./shaders/shader.vert";
        private string fragmentPath = "./shaders/shader.frag";

        public Triangle(float[] vertices)
        {
            this.vertices = vertices;
        }

        public void Load()
        {
            this.vertexBufferHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.vertexBufferHandle);
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                vertices.Length * sizeof(float),
                vertices,
                BufferUsageHint.StaticDraw
            );
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            this.vertexArrayHandle = GL.GenVertexArray();
            GL.BindVertexArray(this.vertexArrayHandle);
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.vertexBufferHandle);
            GL.VertexAttribPointer(
                0,
                3,
                VertexAttribPointerType.Float,
                false,
                3 * sizeof(float),
                0
            );
            GL.EnableVertexAttribArray(0);
            GL.BindVertexArray(0);

            string vertexShaderCode = File.ReadAllText(vertexPath);

            string frammentShadeCode = File.ReadAllText(fragmentPath);

            int vertexShaderHandle = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderHandle, vertexShaderCode);
            GL.CompileShader(vertexShaderHandle);
            int fragmentShaderHandle = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShaderHandle, frammentShadeCode);
            GL.CompileShader(fragmentShaderHandle);
            this.shaderProgramHandle = GL.CreateProgram();
            GL.AttachShader(this.shaderProgramHandle, vertexShaderHandle);
            GL.AttachShader(this.shaderProgramHandle, fragmentShaderHandle);
            GL.LinkProgram(this.shaderProgramHandle);
            GL.DetachShader(this.shaderProgramHandle, vertexShaderHandle);
            GL.DetachShader(this.shaderProgramHandle, fragmentShaderHandle);
            GL.DeleteShader(vertexShaderHandle);
            GL.DeleteShader(fragmentShaderHandle);
        }

        public void Render()
        {
            GL.UseProgram(this.shaderProgramHandle);
            GL.BindVertexArray(this.vertexArrayHandle);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }

        public void Unload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(this.vertexBufferHandle);
            GL.UseProgram(0);
            GL.DeleteProgram(this.shaderProgramHandle);
        }
    }
}
