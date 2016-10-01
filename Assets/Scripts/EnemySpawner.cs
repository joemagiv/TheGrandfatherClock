using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public float timeSinceEnemyLastSpawned;

	public float enemySpawnTime;

	public bool alexanderActive = false;

	public GameObject alexanderPrefab;

	// Use this for initialization
	void Start () {
		RandomizeSpawnTime ();
	}

	void RandomizeSpawnTime(){
		enemySpawnTime = Random.Range(5.0f,10.0f);

	}

	void SpawnEnemy(){
		GameObject Alexander = Instantiate (alexanderPrefab, transform.position, Quaternion.identity, transform) as GameObject;
		EnemyMovement enemyMovement = Alexander.GetComponentInChildren<EnemyMovement> ();
		enemyMovement.enemyActive = true;
		timeSinceEnemyLastSpawned = 0;
		RandomizeSpawnTime ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!alexanderActive) {
			timeSinceEnemyLastSpawned += Time.deltaTime;
			if (timeSinceEnemyLastSpawned > enemySpawnTime) {
				SpawnEnemy ();
				alexanderActive = true;
				timeSinceEnemyLastSpawned = 0;
			}
		}
	}
}
