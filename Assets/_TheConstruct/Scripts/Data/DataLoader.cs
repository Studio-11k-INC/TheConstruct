using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLoader 
{
    static string RootPath;
    static string DataPath;

    static string DataExtrusions = "/Extrusions";
    static string jsonFile = "geometry.json";
    static string cameraPositions = "camPos.json";

    public static List<Extrusion> Extrusions;
    public static CameraPositions CameraPositions = null;
    static List<Wall> Walls;

    public static Vector2 Dimensions;
    public DataLoader(string dataPath)
    {
        RootPath = Application.dataPath;
        DataPath = $"{RootPath}/{dataPath}";
    }

    public static void LoadExtrusions()
    {
        if (Extrusions == null)
            Extrusions = new List<Extrusion>();
        else
            Extrusions.Clear();

        List<string> dirs = new List<string>(Directory.EnumerateDirectories(DataPath + DataExtrusions));
        
        foreach(string dir in dirs)
        {
            string fileName = $@"{dir}/{jsonFile}";

            try
            {
                FileStream stream = File.Open(fileName, FileMode.Open);
                StreamReader reader;

                reader = new StreamReader(stream);
                string json = reader.ReadToEnd();
                reader.Close();
                stream.Close();

                Extrusion extrusion  = JsonConvert.DeserializeObject<Extrusion>(json);
                extrusion.path = dir;
                Extrusions.Add(extrusion);

                Dimensions = new Vector2(extrusion.width, extrusion.height);
            }
            catch (Exception e)
            {
                Debug.LogError("File: " + fileName + " COULD NOT BE FOUND!!");
            }

            fileName = $@"{dir}/{cameraPositions}";

            try
            {
                FileStream stream = File.Open(fileName, FileMode.Open);
                StreamReader reader;

                reader = new StreamReader(stream);
                string json = reader.ReadToEnd();
                reader.Close();
                stream.Close();

                CameraPositions = JsonConvert.DeserializeObject<CameraPositions>(json);                
            }
            catch (Exception e)
            {
                Debug.Log("File: " + fileName + " COULD NOT BE FOUND!!");
            }
        }
    }    

    public static void CreateWalls()
    {
        if (Walls == null)
            Walls = new List<Wall>();
        else
            Walls.Clear();

        foreach (Extrusion extrusion in Extrusions)
        {
            foreach (Vector2[] segments in extrusion.wallSegments)
            {
                Wall wall = new Wall(segments);
                Walls.Add(wall);
            }
        }
    }

    public static List<Wall> GetWallData()
    {
        return Walls;
    }

    public static CameraPositions GetCameraPosition()
    {
        return CameraPositions;
    }

    public static void SaveCameraPositions(CameraPositions camPositions)
    {
        CameraPositions = camPositions;
        string positions = JsonConvert.SerializeObject(camPositions);

        string fileName = $@"{DataPath + DataExtrusions}/test/{cameraPositions}";

        try
        {
            FileStream stream = File.Open(fileName, FileMode.OpenOrCreate);
            StreamWriter writer;

            writer = new StreamWriter(stream);
            writer.Write(positions);           
            
            writer.Close();
            stream.Close();

        }
        catch(Exception e)
        {

        }
     }
}
