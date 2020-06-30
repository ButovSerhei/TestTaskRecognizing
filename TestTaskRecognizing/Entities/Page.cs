using System;
using System.Collections.Generic;

namespace TestTaskRecognizing.Entities
{
    public class Page
    {
        public PageHeader Header { get; set; }            //have to contain the structure of phrase FUT HUB > NEW ITEM

        public virtual string FindShortestRoute(int startPosition)
        {return String.Empty;}

    }
}