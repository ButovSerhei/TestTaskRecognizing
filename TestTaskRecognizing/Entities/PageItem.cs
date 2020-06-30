namespace TestTaskRecognizing.Entities
{
    public abstract class Item
    {
        public int Width { get; set; }         // Container for width pixels count 
        public int Height { get; set; }        // Container for height pixels count 
        public int indentX { get; set; }         //SB: Indent to object from edge of image on X
        public int  indentY { get; set; }     //SB: Indent to object from edge of image on Y
        public virtual int Square { get; set; }
        
    }
}