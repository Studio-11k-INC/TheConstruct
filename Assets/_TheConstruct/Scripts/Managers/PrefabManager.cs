using System;
using UnityEngine;

[Serializable]
public class PrefabPair
{
	public ePrefabType Type;
	public GameObject Prefab;	
	public Transform Parent;
}

public class PrefabManager : MonoBehaviour
{
	public static PrefabManager Instance;

	[SerializeField]
	public PrefabPair[] PrefabPairs;	

	// Start is called before the first frame update

	private void Awake()
	{
		Instance = this;
	}

	public PrefabPair GetPrefabPairByType(ePrefabType type)
	{
		PrefabPair retVal = null;
		foreach(PrefabPair pair in PrefabPairs)
		{
			if(pair.Type == type)
			{
				retVal = pair;
				break;
			}
		}

		return retVal;
	}

	public void SetPrefabPairParentByType(ePrefabType type, Transform parent)
	{
		foreach (PrefabPair pair in PrefabPairs)
		{
			if (pair.Type == type)
			{
				pair.Parent = parent;
				break;
			}
		}
	}
}
