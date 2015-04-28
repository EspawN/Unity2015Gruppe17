using UnityEngine;
using System.Collections;

public class SmallMarioBehaviour : MonoBehaviour {
	float stdSpeed = 0.9f;
	float walkSpeed = 0.9f;
	public KeyCode runButton;
	float runningSpeed = 1.8f;
	bool walking;
	bool idle;
	public float jumpingPower = 3.7f;
	Animator anim;
	Rigidbody2D rb;
	bool facingRight = true;
	bool flipped;
	float moveDir;
	private int state = 1;	
	public Sprite deadSprite;
	private SpriteRenderer spriteRenderer;	
	private float enemyBounceOfForce = 2.2f;

	// groundCheck
	bool notJumping;
	public Transform groundCheck;
	public LayerMask ground;
	// groundCheck end
	void Awake () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	void Update(){

		moveDir = Input.GetAxis ("Horizontal");
		notJumping = Physics2D.OverlapCircle (groundCheck.position, 0.2f, ground);
		if (Input.GetKey (runButton) && notJumping && Mathf.Abs(moveDir) > 0) {
			stdSpeed = runningSpeed;
			// addded this test to maintain velocity while jumping
		} else if(notJumping) {
			stdSpeed = walkSpeed;
		}// added this test so it keeps current velocity
		if (Input.GetButtonDown ("Jump") && notJumping) {
			Debug.Log("cam" + Camera.main.transform.position);
			rb.velocity = transform.up*jumpingPower;
			notJumping = false;
		}
		DoAnimation ();
	}
	void DoAnimation(){
		anim.SetBool("isJumping",!notJumping);
		if (notJumping) {
			CheckForMoveDirection (moveDir);
			anim.SetFloat ("isWalking", Mathf.Abs (moveDir * stdSpeed));
		} 
	}
	void FixedUpdate () {
		rb.velocity = new Vector2 (moveDir * stdSpeed,rb.velocity.y);
		//rigidbody2D.position = new Vector2(
		//	Mathf.Clamp(rigidbody2D.position.x, xMin, xMax), 
		//	Mathf.Clamp(rigidbody2D.position.y, yMin, yMax)
		//	);
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
			GoombaBehaviour goombaBehave = source.GetComponent<GoombaBehaviour>();
			if(goombaBehave.Isalive()){
				KillPlayer();
			} else{
				// bounce of enemy
				rb.velocity = transform.up*enemyBounceOfForce;
			}
		} else if (other.gameObject.tag == "PowerUp") {
			UpTheAnte();
		}
	}
	void KillPlayer(){
		this.anim.enabled = false;
		this.spriteRenderer.sprite = deadSprite;
		rb.velocity = transform.up*jumpingPower;
	}
	// mario has 3 states in this game small,big,fire -> 1,2,3
	void UpTheAnte(){
		state = 2;

	}
	public int getState(){
		return state;
	}
	public void setDefaultState(){
		this.state = 1;
	}
}
