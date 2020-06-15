using Doozy.Engine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsManager : BaseMono
{
    public static WallsManager Instance;   

    public override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    public bool AllWalls;
    public int StartWallIndex;
    public int EndWallIndex;

    public float VertsMult;
    public float MaxWallHeight;
    public float GrowTime;
    float YMag;
    // Start is called before the first frame update
    void Start()
    {
        YMag = MaxWallHeight * VertsMult;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateWalls()
    {
        PrefabPair pair = PrefabManager.Instance.GetPrefabPairByType(ePrefabType.WALL);        

        pair.Parent.position = new Vector3(-(DataLoader.Dimensions.x * VertsMult)/2, 0.0f, (DataLoader.Dimensions.y * VertsMult)/2);
        List<Wall> walls = DataLoader.GetWallData();

        if (AllWalls)
        {
            StartWallIndex = 0;
            EndWallIndex = walls.Count;
        }

        for(int i = StartWallIndex; i < EndWallIndex; i++)
        {
            Wall wall = walls[i];

            wall.InitVerts(wall.Segments, VertsMult, YMag);
        
            GameObject go = Instantiate(pair.Prefab, pair.Parent);
            go.name = $"Wall {pair.Parent.childCount}";
            MeshFilter filter = go.GetComponent<MeshFilter>();
            filter.mesh = wall.GetMesh();

            WallObj obj = go.GetComponent<WallObj>();
            obj.Init(YMag, GrowTime);
        }
    }

    public void WallsStart()
    {
        GameEventMessage.SendEvent(eMessages.WALL_GROW.ToString());
    }

    public void WallsGrow()
    {

    }

    public void WallsFullHeight()
    {
        GameEventMessage.SendEvent(eMessages.WALL_GROW.ToString());
    }

    public void WallsStop()
    {

    }
}
