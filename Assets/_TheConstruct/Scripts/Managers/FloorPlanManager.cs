using Doozy.Engine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPlanManager : BaseMono
{
    public static FloorPlanManager Instance;

    public float ScaleMult;
    public Color StartColor;
    public Color EndColor;
    public float FadeInStep;

    public override void Awake()
    {
        base.Awake();
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateFloorPlan(Extrusion extrusion)
    {
        PrefabPair pair = PrefabManager.Instance.GetPrefabPairByType(ePrefabType.FLOORPLAN);
        GameObject go = Instantiate(pair.Prefab, pair.Parent);

        float scale = WallsManager.Instance.VertsMult * ScaleMult;
        go.transform.localScale = new Vector3(scale, scale, 1.0f);

        SpriteRenderer renderer = go.GetComponent<SpriteRenderer>();
        renderer.sprite = Sprite.Create((Texture2D)extrusion.floorPlan, new Rect(0.0f, 0.0f, extrusion.floorPlan.width, extrusion.floorPlan.height), new Vector2(0.5f, 0.5f), 100.0f);

        FloorPlanObj obj = go.GetComponent<FloorPlanObj>();
        obj.Init(StartColor, EndColor, FadeInStep);
    }

    public void FloorPlanStart()
    {
        GameEventMessage.SendEvent(eMessages.FLOORPLAN_FADEIN.ToString());
    }

    public void FloorPlanFadeIn()
    {

    }

    public void FloorPlanFadeInDone()
    {
        GameEventMessage.SendEvent(eMessages.FLOORPLAN_STOP.ToString());
    }

    public void FloorPlanStop()
    {
        GameEventMessage.SendEvent(eMessages.WALL_START.ToString());
    }
}
