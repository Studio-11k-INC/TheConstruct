using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    public static LoadManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    public string DataPath;

    DataLoader DataLoader;

    // Start is called before the first frame update
    void Start()
    {
        DataLoader = new DataLoader(DataPath);
        DataLoader.LoadExtrusions();
        DataLoader.CreateWalls();

        StartCoroutine(ImageManager.Instance.LoadExtrustionsImages());        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
