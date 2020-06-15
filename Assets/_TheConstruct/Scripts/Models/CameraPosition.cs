using System.Collections.Generic;

public class MyVector
{
    public string x { get; set; }
    public string y { get; set; }
    public string z { get; set; }
    public string w { get; set; }
}
public class CameraPosition
{
    public MyVector PivotRotation { get; set; }
    public MyVector Position { get; set; }
}

public class CameraPositions
{
    public List<CameraPosition> Positions { get; set; }

    public CameraPositions()
    {
        Positions = new List<CameraPosition>();
    }
}
