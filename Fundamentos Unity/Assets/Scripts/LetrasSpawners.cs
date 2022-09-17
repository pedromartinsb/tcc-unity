using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetrasSpawners : MonoBehaviour {

	double[] pos1 = {2.81,-3.64,14.56};
	double[] pos2 = {5.97,19.34,30.244};
	double[] pos3 = {-6.34,3.55, 27.57};
	Vector2 localdeSpawn;
	Vector2 local;
	public GameObject letra;
	public GameObject img1;
	public GameObject img2;
	// Use this for initialization
	void Start () {
		Spawn ();
	}

	// Update is called once per frame


	void Spawn ()
	{
		//Random randNum = new Random();
		//
		float i = Random.Range(1.0f,4.0f);
		Debug.Log ("POSICAO I : " + i);
		if (i < 2.0f) {
			
			float x = Random.Range (-3.64f,14.56f);

			localdeSpawn = new Vector2(x,2.81f);
			local = new Vector2(x + 0.9f,2.81f);
			Instantiate (letra, localdeSpawn, Quaternion.identity);
			Instantiate (img1, local, Quaternion.identity);

		} else if (i < 3.0f) {
			
			float x = Random.Range (19.34f,30.244f);
			localdeSpawn = new Vector2(x,5.97f);
			local = new Vector2(x + 0.9f,5.97f);
			Instantiate (letra, localdeSpawn, Quaternion.identity);
			Instantiate (img2, local, Quaternion.identity);

		} else {
			
			float x = Random.Range (3.55f,27.57f);
			localdeSpawn = new Vector2(x,-6.34f);
			local = new Vector2(x + 0.9f,-6.34f);
			Instantiate (letra, localdeSpawn, Quaternion.identity);
			Instantiate (img1, local, Quaternion.identity);

		}
	}
}
