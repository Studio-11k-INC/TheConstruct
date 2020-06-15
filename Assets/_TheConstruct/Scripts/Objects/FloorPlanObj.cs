using Doozy.Engine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPlanObj : BaseMono
{
    public SpriteRenderer SpriteRenderer;
    // Start is called before the first frame update
    Color StartColor;
    Color EndColor;    
    float FadeTime;
    float Progress;

    eFloorPlanState State = eFloorPlanState.IDLE;

    public override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (State == eFloorPlanState.FADEIN)
        {
            Progress += (Time.deltaTime / FadeTime);
            SpriteRenderer.color = Color.Lerp(StartColor, EndColor, Progress);

            enabled = SpriteRenderer.color != EndColor;

            if (!enabled)
            {
                Debug.Log($"Fade Time{Time.time}");
                State = eFloorPlanState.IDLE;
                GameEventMessage.SendEvent(eMessages.FLOORPLAN_FADEIN_DONE.ToString());
            }
        }

    }

    public void Init(Color startColor, Color endColor, float fadeTime)
    {        
        StartColor = startColor;
        EndColor = endColor;        
        FadeTime = fadeTime;
        Progress = 0.0f;        

        SpriteRenderer.color = StartColor;        
    }
    
    public void StartEvent()
    {
        State = eFloorPlanState.FADEIN;
    }
}
