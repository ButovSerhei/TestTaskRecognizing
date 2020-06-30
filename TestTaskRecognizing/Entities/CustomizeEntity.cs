using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TestTaskRecognizing.Entities
{
    public class CustomizeEntity : Page
    {
        public ICollection<CustomItemTile> Tiles { get; set; }

        public override string FindShortestRoute(int startPosition)
        {
            string instructions = string.Empty;

            var tilePosition = Tiles.FirstOrDefault(x => x.IsActive).Position -1 ; 
            startPosition--;

            int rows, cols, start, finish;
            start = startPosition > tilePosition ? startPosition : tilePosition;
            finish = startPosition > tilePosition ? tilePosition : startPosition;

            rows = start / 4; 
            cols = start - 4 * rows - finish;

            for (int i = Math.Abs(rows); i != 0; i--)
            {
                instructions += rows > 0 ? " Up " : " Down ";
            }

            for (int i = Math.Abs(cols); i != 0; i--)
            {
                instructions += cols < 0 ? " Right " : " Left ";
            }
            return string.IsNullOrEmpty(instructions) ? " It's active tile " : instructions;
        }

    }

    public class CustomItemTile : Tile
    {
        private int startXPoint = 120, startYPoint = 150;
        private int xThreshold => 120 + (Width * 4);
        private int yThreshold => 150 + (Height * 2);

        public override int Position => this.CalculatePosition();


        private new int CalculatePosition()
        {
            int position = 0;

            for (int i = startYPoint; i <= yThreshold; i += Height)
            {
                for (int j = startXPoint; j <= xThreshold; j += Width)
                {
                    if (indentX < j && indentY < i)
                    { return position; }

                    position++;
                }
            }

            return position;
        }

        public CustomItemTile(int height, int width, bool isActive) : base(height, width, isActive)
        {
        }
    }
}