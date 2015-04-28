using UnityEngine;
using System.Collections;

public class QuestionBoxHit : MonoBehaviour {
	public Sprite deadSprite;
	public GameObject powerPrefab;
	// default 1 power i box
	public int powersInBox = 1;
	private int powersInstantiated = 0;
	private SpriteRenderer spriteRenderer;
	private Animator anim;
	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		// just to reduce a script we implement this check so object with no animator
		// dont give error message.
		if(gameObject.GetComponent<Animator>() != null){
		anim = GetComponent<Animator> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnTriggerEnter2D(Collider2D other){
			if (powersInstantiated < powersInBox) {
			Instantiate (powerPrefab, new Vector2 (transform.position.x, transform.position.y + 0.01f), transform.rotation);
			powersInstantiated++;
		} if(powersInstantiated == powersInBox) {
			if(anim != null)
			anim.enabled = false;
			spriteRenderer.sprite = deadSprite;
		}
	}
}
