namespace TestTaskRecognizing.Entities
{
    public interface IItem
    {
         int Width { get; set; }         // Container for width pixels count 
         int Height { get; set; }        // Container for height pixels count 
         int indentX { get; set; }         //SB: Indent to object from edge of image on X
         int  indentY { get; set; }     //SB: Indent to object from edge of image on Y
          int Square { get; set; }
        
    }
}