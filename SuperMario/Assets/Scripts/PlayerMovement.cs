using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	float speed = 0.9f;
	public float jumpingPower = 100;
	public KeyCode runButton;
	Animator anim;
	Rigidbody2D rb;
	BoxCollider2D boxCollider;
	CircleCollider2D circleCollider;
	bool facingRight = true;
	bool flipped;
	bool notJumping;
	float moveDir;
	private float state = 1;

	public Sprite deadSprite;
	public Sprite bigSprite;
	private SpriteRenderer spriteRenderer;

	private GoombaBehaviour goombaBehave;
	
	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		//col = GetComponent<Collider2D> ();
		circleCollider = GetComponent<CircleCollider2D> ();
		boxCollider = GetComponent<BoxCollider2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	void Update(){
		notJumping = rb.velocity.y == 0;
		if (Input.GetButtonDown ("Jump") && notJumping) {
			rb.AddForce (new Vector2(0,jumpingPower));
		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		moveDir = Input.GetAxis("Horizontal");
		if(notJumping)
		CheckForMoveDirection (moveDir);
		rb.velocity = new Vector2 (moveDir * speed,rb.velocity.y);
	//	if (notJumping) {
	//		anim.SetFloat ("IsWalking", Mathf.Abs (move));
	//	}
		if (Input.GetKey(runButton)) {
			Debug.Log ("running");
			anim.SetBool ("isRunning", true);
		} else {
			anim.SetBool("isRunning",false);
		}
	}
	void CheckForMoveDirection(float move){
		if (move < 0) {
			if(facingRight && !flipped)
				FlipPlayer();
			flipped = true;
		}
		if (move > 0 && flipped) {
			FlipPlayer();
			flipped = false;
		}
	}
	
	void FlipPlayer(){
		transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
	}
	void OnCollisionEnter2D(Collision2D other){
		GameObject source = other.gameObject;
		if (source.tag == "Enemy") {
			goombaBehave = source.GetComponent<GoombaBehaviour>();
			Debug.Log ("hit by enemy");
			state -=1;
			 if(state == 0 && goombaBehave.Isalive()){
			KillPlayer();
			}
		} else if (other.gameObject.tag == "PowerUp") {
			GoBig();
		}
	}
	void KillPlayer(){
			this.anim.enabled = false;
			this.spriteRenderer.sprite = deadSprite;
			rb.AddForce (new Vector2 (0, 100));
			//Destroy (gameObject, 2);
	}
	// mario has 3 states in this game small,big,fire -> 1,2,3
	void GoBig(){
		Debug.Log("Powering up");
		if(getState() != 2)
		state += 1;
		if (state == 2) {
			anim.enabled = false;
			this.spriteRenderer.sprite = bigSprite;

			boxCollider.size = new Vector2(0.1f,0.1f);
			boxCollider.offset = new Vector2(0,0.1f);

			circleCollider.radius = 0.09f;
			circleCollider.offset = new Vector2(circleCollider.offset.x,-0.06f);
		}
	}
	private float getState(){
		return state;
	}
}
