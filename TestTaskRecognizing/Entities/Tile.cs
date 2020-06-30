using System;

namespace TestTaskRecognizing.Entities
{
    public class Tile : Item
    {
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