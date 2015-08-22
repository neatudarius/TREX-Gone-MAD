﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmallBackgroundSpawner : MonoBehaviour {

	public List<GameObject> smallBackgroundTilePrefabs;
	public GameObject rightmostBuilding;
	
	private float spawnTimer = 0;
	private Vector3 cornerOfNewestBuilding;
	private GameObject globalManager;
	
	// Use this for initialization
	void Start () {
		globalManager = GameObject.Find ("_GlobalManager");
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTimer < 0) {
			// Choose a random building
			int randomIndex = Random.Range(0, smallBackgroundTilePrefabs.Count);
			GameObject randomPrefab = smallBackgroundTilePrefabs[randomIndex];
			
			// Spawn the building and position it
			GameObject newBuilding = Instantiate (randomPrefab);
			float newWidth = newBuilding.GetComponent<BackgroundTile>().GetWidth () - 0.1f;
			float oldWidth = rightmostBuilding.GetComponent<BackgroundTile>().GetWidth();
			newBuilding.transform.position = rightmostBuilding.transform.position + new Vector3( (newWidth + oldWidth) / 2, 0, 0 );
			newBuilding.transform.parent = GameObject.Find ("SmallBackgroundHolder").transform;
			rightmostBuilding = newBuilding;
			
			// Calculate time until next spawn
			spawnTimer = newWidth / globalManager.GetComponent<GlobalManager>().backgroundSpeed;
		}
		
		spawnTimer -= Time.deltaTime;
	}
}