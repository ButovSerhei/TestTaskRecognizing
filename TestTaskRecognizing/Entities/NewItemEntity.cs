using System;
using System.Collections.Generic;

namespace TestTaskRecognizing.Entities
{
    public class NewItemEntity : Page
    {
        public ICollection<NewItemTile> Tiles { get; set; }
    }

    public class NewItemTile : Tile
    {


        public NewItemTile(int height, int width, bool isActive) : base(height, width, isActive)
        {
         
        }
    }
   
}