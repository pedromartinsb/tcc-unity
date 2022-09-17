using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetrasSpawnersM2 : MonoBehaviour {
     
	Vector2 localdeSpawn;
	Vector2 local;
	public GameObject letra;
	// Use this for initialization
	void Start () {
		Spawn ();
	}

	// Update is called once per frame


	void Spawn ()
	{
		//Random randNum = new Random();
		//
		float i = Random.Range(1.0f,6.0f);
		Debug.Log ("POSICAO I : " + i);
		if (i < 2.0f) {
			
			float x = Random.Range (50.54f,63.86f);

			localdeSpawn = new Vector2(x,11.36f);
			Instantiate (letra, localdeSpawn, Quaternion.identity);
		} else if (i < 3.0f) {
			
			float x = Random.Range (55.15f,66.17f);
			localdeSpawn = new Vector2(x,5.73f);
			Instantiate (letra, localdeSpawn, Quaternion.identity);
		} else if (i < 4.0f) {
			
			float x = Random.Range (66.71f,68.06f);
			localdeSpawn = new Vector2(x,11.24f);
			Instantiate (letra, localdeSpawn, Quaternion.identity);
		}else if (i < 5.0f){
			float x = Random.Range (53.68f,59.91f);
			localdeSpawn = new Vector2(x,4.29f);
			Instantiate (letra, localdeSpawn, Quaternion.identity);
		}else{
			float x = Random.Range (61.17f,64.67f);
			localdeSpawn = new Vector2(x,3f);
			Instantiate (letra, localdeSpawn, Quaternion.identity);
		}
	}
}
