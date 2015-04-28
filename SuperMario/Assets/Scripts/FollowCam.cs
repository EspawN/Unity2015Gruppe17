using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
	public GameObject smallMario;
	public GameObject largeMario;
	private float initialOffSet;

	// Update is called once per frame
	void Start(){
		initialOffSet = 0.9f;
		transform.position = new Vector3 (smallMario.transform.position.x + initialOffSet, transform.position.y, -10);
	}
	void Update () {
		GameObject currGameObject;
		int current = currentObject ();
		if (current == 1) {
			currGameObject = smallMario;
		} else
			currGameObject = largeMario;
			if(currGameObject.transform.position.x > transform.position.x-0.1f)
			transform.position = new Vector3 ((currGameObject.transform.position.x+0.1f), transform.position.y, -10);
		
	}
	int currentObject(){
		if (smallMario.activeInHierarchy) {
			return 1;
		}
		else if (largeMario.activeInHierarchy) {
			return 2;
		}
		return 3;
	}
}
