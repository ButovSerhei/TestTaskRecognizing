using System;
using System.Collections.Generic;

namespace TestTaskRecognizing.Entities
{
    public class NewItemEntity : Page
    {
    }

    public class NewItemPale : Tile
    {
        public override int Square => Height * Width;
        
        public NewItemPale(int height, int width, bool isActive) : base(height, width, isActive)
        {
            if (!isActive)
            {
                indentX = Convert.ToInt32(width / 17.45);             //SB: proportion to resolution
                indentY = Convert.ToInt32(height / 1.37);             //SB: proportion to resolution
                Height = Convert.ToInt32(height / 9);                 //SB: proportion to resolution
                Width = Convert.ToInt32(width / 1.13);                //SB: proportion to resolution
            }
            else
            {
            }
        }
    }
   
}