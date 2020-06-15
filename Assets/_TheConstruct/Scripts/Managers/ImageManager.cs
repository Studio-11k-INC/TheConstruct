using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;
using Doozy.Engine;

public class ImageManager : BaseMono
{
	public static ImageManager Instance;
	public override void Awake()
	{
		base.Awake();
		Instance = this;
	}

	public Texture TextureDefault;

	public IEnumerator LoadExtrustionsImages()
    {
		foreach(Extrusion extrusion in DataLoader.Extrusions)
        {
			StartCoroutine(LoadTextures(extrusion));
        }

		yield return true;
	}
	
	public IEnumerator LoadTextures(Extrusion extrusion)
	{
		string dataFolder = extrusion.path;
		var allowedExtensions = new[] { ".jpeg", ".png", ".jpg" };
		var temp = Directory
			.GetFiles(dataFolder)
			.Where(file => allowedExtensions.Any(file.ToLower().EndsWith))
			.ToList();

		string[] files = null;
		files = temp.ToArray();

		if (files.Length > 0)
		{
			if (!string.IsNullOrEmpty(files[0]))
				StartCoroutine(LoadActorTexture(files[0], extrusion));
			else
				extrusion.floorPlan = TextureDefault;
		}
		else
			extrusion.floorPlan = TextureDefault;

		yield return true;
	}

	public IEnumerator LoadActorTexture(string fileName, Extrusion extrusion)
	{
		WWW www = new WWW(fileName);
		yield return www;

		extrusion.floorPlan = www.texture;

		FloorPlanManager.Instance.CreateFloorPlan(extrusion);
		WallsManager.Instance.CreateWalls();

		GameEventMessage.SendEvent(eMessages.FLOORPLAN_START.ToString());
	}
}