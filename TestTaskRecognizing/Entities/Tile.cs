using System;

namespace TestTaskRecognizing.Entities
{
    public class Tile : IItem
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int indentX { get; set; }
        public int indentY { get; set; }
        public int Square { get; set; }
        public string Name { get; set; }
        public virtual int Position => CalculatePosition();

        public virtual int CalculatePosition()
        {
            return 0;
        }

        public bool IsActive { get; set; }
        
        public Tile(int height, int width, bool isActive)
        {
            Height = height;
            Width = width;
            IsActive = isActive;
        }

        
    }
}