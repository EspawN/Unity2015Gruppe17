using UnityEngine;
using System.Collections;

public class BigMarioBehaviour : MonoBehaviour {
	float stdSpeed = 0.9f;
	float walkSpeed = 0.9f;
	float runningSpeed = 1.8f;

	public KeyCode runButton;
	Animator anim;
	Rigidbody2D rb;
	bool facingRight = true;
	bool flipped;
	float moveDir;
	// state 2 = large
	private int state = 2;
	private GoombaBehaviour goombaBehave;

	// groundCheck and Jumping
	bool notJumping;
	public float jumpingPower = 3.7f;
	public Transform groundCheck;
	public LayerMask ground;
	private float enemyBounceOfForce = 2.2f;
	// groundCheck end
	
	void Awake () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
	}
	void Update(){
		moveDir = Input.GetAxis ("Horizontal");
		notJumping = Physics2D.OverlapCircle (groundCheck.position, 0.2f, ground);
		if (Input.GetKey (runButton) && notJumping && Mathf.Abs(moveDir) > 0) {
			stdSpeed = runningSpeed;
		} else if(notJumping) {
			stdSpeed = walkSpeed;
		}
		if (Input.GetButtonDown ("Jump") && notJumping) {
			rb.velocity = transform.up*jumpingPower;
			notJumping = false;
		}
		DoAnimation ();
	}
	void DoAnimation(){
		anim.SetBool("isJumping",!notJumping);
		if (notJumping) {
			CheckForMoveDirection (moveDir);
			// hvis farten er større enn abs 0.9 da løper vi
			anim.SetFloat ("isWalking", Mathf.Abs (moveDir * stdSpeed));
		} 
	}
	void FixedUpdate () {
		rb.velocity = new Vector2 (moveDir * stdSpeed,rb.velocity.y);
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
			if(goombaBehave.Isalive()){
				KillPlayer();
				Destroy(source);
			} else{
				// bounce of enemy
				rb.velocity = transform.up*enemyBounceOfForce;
			}
		} else if (other.gameObject.tag == "PowerUp") {
			UpTheAnte();
		}
	}
	void KillPlayer(){
			state = 1;
	}
	// mario has 3 states in this game small,big,fire -> 1,2,3
	void UpTheAnte(){
		Debug.Log("needs input for fireMario");
	}
	public int getState(){
		return state;
	}
	public void setDefaultState(){
		this.state = 2;
	}
}
