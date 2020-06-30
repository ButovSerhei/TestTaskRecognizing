using System;

namespace TestTaskRecognizing.Entities
{
    public class PageHeader : Item
    {
        public override int Square  { get; set; }

        public PageHeader(int height, int width)
        {
            Width = Convert.ToInt32(width / 6.5);             //SB: proportion to resolution
            Height = Convert.ToInt32(height / 23.47);             //SB: proportion to resolution
            indentX = Convert.ToInt32(width / 20);                 //SB: proportion to resolution
            indentY = Convert.ToInt32(height / 11.7);                //SB: proportion to resolution
        }
    }
}