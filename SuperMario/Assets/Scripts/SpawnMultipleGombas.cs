using UnityEngine;
using System.Collections;

public class SpawnMultipleGombas : MonoBehaviour {
	public GameObject gomba;
	private bool spawned;
	public Transform []transforms;

	void OnTriggerEnter2D(){
		if (!spawned) {
			for(int i = 0; i < transforms.Length; i++){
				Instantiate(gomba,transforms[i].position,Quaternion.identity);
			}
		}
		spawned = true;
	}
}
