using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEngine;

public class Wall 
{   
    public int length;
    public Vector3[] verts3d;
    public Vector2[] verts2d;
    public int[] tris;    

    public Vector2[] Segments;    

    float VertsMult;
    float YMag;

    public Wall(Vector2 [] segments)    
    {
        Segments = segments.Take(segments.Length -1).ToArray();        
    }

    public void InitVerts(Vector2[] verts, float vertsMult = 0.3048f, float yMag = 2.7432f)
    {
        VertsMult = vertsMult;
        YMag = yMag;

        SetVerts(verts);
    }

    public void SetVerts(Vector2[] verts)
    {
        verts2d = verts;
        length = verts.Length;
        verts3d = new Vector3[length * 5];
        tris = new int[length * 6];

        for (int i = 0; i < length; i++)
        {
            Vector2 vert = verts[i] * VertsMult;
            Vector2 vert2;
            bool notLast = i != length - 1;
            if (notLast)
            {
                vert2 = verts[i + 1] * VertsMult;
            }
            else
            {
                vert2 = verts[0] * VertsMult;
            }
            verts3d[i * 4] = new Vector3(vert.x, 0, -vert.y);
            verts3d[i * 4 + 1] = new Vector3(vert.x, YMag, -vert.y);
            verts3d[i * 4 + 2] = new Vector3(vert2.x, 0, -vert2.y);
            verts3d[i * 4 + 3] = new Vector3(vert2.x, YMag, -vert2.y);
            verts3d[i + length * 4] = new Vector3(vert.x, YMag, -vert.y);//last set so all the verts line up as they where give for the top

            //walls
            tris[i * 6] = i * 4;
            tris[i * 6 + 1] = i * 4 + 1;
            tris[i * 6 + 2] = i * 4 + 2;
            tris[i * 6 + 3] = i * 4 + 1;
            tris[i * 6 + 4] = i * 4 + 3;
            tris[i * 6 + 5] = i * 4 + 2;
        }
        //for top using a file off the wiki cause this is a pita
        Triangulator tr = new Triangulator(verts2d);
        int[] indices = tr.Triangulate();

        for (int i = 0; i < indices.Length; i++)
        {
            indices[i] += length * 4;
        }

        //merge top
        List<int> tempTris = tris.ToList<int>();
        tempTris.AddRange(indices);
        tris = tempTris.ToArray();
    }

    public Mesh GetMesh()//this cant be parallelized so its left out
    {
        var mesh = new Mesh();
        mesh.vertices = verts3d;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        mesh.Optimize();
        return mesh;
    }
}
