using UnityEngine;

public class WallObj : BaseMono
{
    float StartHeight;
    float EndHeight;
    float GrowTime;

    public float NormalMod = 1.0f;
    public bool ShowGrowTimes;

    eWallState State = eWallState.IDLE;

    public override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GrowTime <= 0.0f)
        {
            Debug.LogError("GrowTime Cannot Be 0.0f");
            return;
        }

        if (State == eWallState.GROW)
        {
            float y = Time.deltaTime * GrowTime;
            transform.Translate(Vector3.up * y);

            enabled = transform.position.y < EndHeight;

            if (!enabled)
            {
                if(ShowGrowTimes)
                    Debug.Log($"Grow Time{Time.time}");
            }
        }
    }
    public void Init(float startHeight, float growTime)
    {
        StartHeight = -startHeight;
        EndHeight = 0.0f;
        GrowTime = startHeight / growTime;        

        transform.position = new Vector3(transform.position.x, StartHeight, transform.position.z);

        State = 0;
    }

    public void StartEvent()
    {
        State = eWallState.GROW;
    }

    public void FlipNormals()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] normals = mesh.normals;

        NormalMod = NormalMod == 1.0f ? -1.0f : 1.0f;

        Debug.Log($"Flip Normals: {NormalMod}");

        for (int i = 0; i < normals.Length; i++)
        {
            if (normals[i] == Vector3.back)
                normals[i] = Vector3.forward;

            if (normals[i] == Vector3.down)
                normals[i] = Vector3.up;

            if (normals[i] == Vector3.left)
                normals[i] = Vector3.right;

            //normals[i] *= NormalMod;           
            Debug.Log($"{i} {normals[i]}");
            //Debug.DrawLine(Vector2.zero, normals[i], Color.red, 2.5f);
        }

        mesh.normals = normals;
    }
}
