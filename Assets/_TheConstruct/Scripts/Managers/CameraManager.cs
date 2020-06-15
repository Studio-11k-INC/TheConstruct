using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : BaseMono
{
    public override void Awake()
    {
        base.Awake();
    }


    public bool Record;
    GameObject MainCamera;

    CameraPositions CameraPositions;
    int CurrentPosition;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        CurrentPosition = 0;       
    }

    // Update is called once per frame
    void Update()
    {
        if(Record)
        {
            if(Input.GetMouseButtonDown(0))
            {
                RecordPosition();
            }
        }
    }

    void RecordPosition()
    {
        Transform camera = MainCamera.transform;
        Transform position = transform;

        Debug.Log($"Manager: [{transform.position}]");
        Debug.Log($"Position: [{camera.position}]");
        Debug.Log($"Position: [{camera.localPosition}]");

        CameraPositions = DataLoader.GetCameraPosition();

        if (CameraPositions == null)
            CameraPositions = new CameraPositions();

        CameraPosition pos = new CameraPosition()
        {
#if false
            PivotRotation = new MyVector()
            {
                x = position.rotation.x.ToString(),
                y = position.rotation.y.ToString(),
                z = position.rotation.z.ToString(),
                w = position.rotation.w.ToString()
            },
#else
            PivotRotation = new MyVector()
            {
                x = camera.rotation.x.ToString(),
                y = camera.rotation.y.ToString(),
                z = camera.rotation.z.ToString(),
                w = camera.rotation.w.ToString()
            },
#endif

            Position = new MyVector()
            {
                x = camera.localPosition.x.ToString(),
                y = camera.localPosition.y.ToString(),
                z = camera.localPosition.z.ToString()
            }
        };

        CameraPositions.Positions.Add(pos);

        DataLoader.SaveCameraPositions(CameraPositions);
    }

    public void StartEvent()
    {

    }

    public void Next()
    {
        Debug.Log("Next");       

        CameraPositions = DataLoader.GetCameraPosition();

        CurrentPosition = CurrentPosition < CameraPositions.Positions.Count - 1 ? CurrentPosition + 1 : 0;

        float x;
        float y;
        float z;
        float w;

        x = float.Parse(CameraPositions.Positions[CurrentPosition].Position.x);
        y = float.Parse(CameraPositions.Positions[CurrentPosition].Position.y);
        z = float.Parse(CameraPositions.Positions[CurrentPosition].Position.z);
        MainCamera.transform.position = new Vector3(x, y, z);

#if false
        x = float.Parse(CameraPositions.Positions[CurrentPosition].PivotRotation.x);
        y = float.Parse(CameraPositions.Positions[CurrentPosition].PivotRotation.y);
        z = float.Parse(CameraPositions.Positions[CurrentPosition].PivotRotation.z);
        w = float.Parse(CameraPositions.Positions[CurrentPosition].PivotRotation.w);
        transform.rotation = new Quaternion(x, y, z, w);        
#else
        x = float.Parse(CameraPositions.Positions[CurrentPosition].PivotRotation.x);
        y = float.Parse(CameraPositions.Positions[CurrentPosition].PivotRotation.y);
        z = float.Parse(CameraPositions.Positions[CurrentPosition].PivotRotation.z);
        w = float.Parse(CameraPositions.Positions[CurrentPosition].PivotRotation.w);
        MainCamera.transform.rotation = new Quaternion(x, y, z, w);
#endif
    }

    public void Previous()
    {

    }

    public void StopEvent()
    {

    }
}
