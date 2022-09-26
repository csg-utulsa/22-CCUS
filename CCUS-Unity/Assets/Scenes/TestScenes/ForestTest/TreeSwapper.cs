using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TreeSwapper : MonoBehaviour
{
    public Terrain terrain;
    public GameObject TreePrefab;
    
    private TreeInstance[] OriginalTerrainData;

    void Start()
    {
        OriginalTerrainData = terrain.terrainData.treeInstances;

        Debug.Log(terrain.terrainData.treeInstanceCount);
		for (int i = 0; i < terrain.terrainData.treeInstanceCount; i++)
		{
            GameObject tree = GameObject.Instantiate(TreePrefab);
            Vector3 pos = terrain.terrainData.treeInstances[i].position;
            tree.transform.position = new Vector3(pos.x * 1000, pos.y * terrain.terrainData.size.y, pos.z * 1000);
        }
        

        TreeInstance[] instances = new TreeInstance[0];
        terrain.terrainData.SetTreeInstances(instances, true);
    }

	private void OnApplicationQuit()
	{
        // Changing tree indices will change the terrain file so they need to be reset back.
        terrain.terrainData.SetTreeInstances(OriginalTerrainData, true);
	}

}
