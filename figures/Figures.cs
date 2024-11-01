namespace Figures
{
    public abstract class Figure
    {
        public abstract void Load();
        public abstract void Render();
        public abstract void Unload();
        private float[] _color = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };

        public float[] Color
        {
            get { return _color; }
            set
            {
                if (value.Length != 4)
                {
                    throw new System.ArgumentException("Color must have 4 components");
                }
                _color = value;
            }
        }
    }
}
