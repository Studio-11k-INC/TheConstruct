using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Extrusion
{
    public int width;
    public int height;
    public List<Vector2[]> wallSegments;

    public string path;
    public Texture floorPlan;
}
