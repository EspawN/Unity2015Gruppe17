using UnityEngine;
using System.Collections;

public class ControlMarios : MonoBehaviour {
	public GameObject lilleMario;
	public GameObject storeMario;
	float mariosBackwardBoundsLimit = 1.67f;

	private SmallMarioBehaviour lilleMarioScript;
	private BigMarioBehaviour storeMarioScript;

	private Vector2 currentPosition;
	private GameObject currentPlayer;

	private int playerState;
	// public GameObject fireMario;

	// tanken er at vi setter store mario til not active fra starten
	// deretter sjekkes state i update på powerups osv
	void Start(){
		lilleMarioScript = lilleMario.GetComponent<SmallMarioBehaviour> ();
		storeMarioScript = storeMario.GetComponent<BigMarioBehaviour> ();
		// default fra start
		currentPlayer = lilleMario;
		playerState = lilleMarioScript.getState ();
	}
	void Update(){
		float camPos = Camera.main.transform.position.x;
		// blokkerer mario fra å gå ut av screenen
		Rigidbody2D rb = currentPlayer.GetComponent<Rigidbody2D> ();
		rb.position = new Vector2 (Mathf.Clamp (rb.position.x, camPos - mariosBackwardBoundsLimit, camPos + 10),rb.transform.position.y);

		if (currentPlayer == lilleMario) {
			playerState = lilleMarioScript.getState ();
			if(playerState == 2){
				PlayerChangeOccured();
				lilleMarioScript.setDefaultState();
			}
		} else if(currentPlayer == storeMario){
			playerState = storeMarioScript.getState();
			if(playerState == 1){
				PlayerChangeOccured();
				storeMarioScript.setDefaultState();
			}
		}
	}
	void PlayerChangeOccured(){
		SetActivePlayer ();
	}
	void SetActivePlayer(){
		if (storeMario.activeInHierarchy) {
			lilleMario.transform.position = storeMario.transform.position;
		} else if (lilleMario.activeInHierarchy) {
			storeMario.transform.position = lilleMario.transform.position;
		}
		if (playerState == 2) {
			lilleMario.SetActive(false);
			storeMario.SetActive(true);
			currentPlayer = storeMario;
		}
		if (playerState == 1) {
			storeMario.SetActive(false);
			lilleMario.SetActive(true);
			currentPlayer = lilleMario;
		}
	}
}