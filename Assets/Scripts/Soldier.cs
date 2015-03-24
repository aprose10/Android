using UnityEngine;
using System.Collections;
/// <summary>
/// Soldiers sprite movements
/// </summary>
public class Soldier: MonoBehaviour {
	private float maxspeed; //walk speed
	Animator anim;
	private bool faceright; //face side of sprite activated
	private bool jumping=false;
	private bool isdead=false;
	private float counter =0;
	private int moveTime = 3;
	private bool stopMoving = false;
	//--
	void Start () {
		maxspeed=3f;//Set walk speed
		faceright=true;//Default right side
		anim = this.gameObject.GetComponent<Animator> ();
		anim.SetBool ("walk", false);//Walking animation is deactivated
		anim.SetBool ("dead", false);//Dying animation is deactivated
		anim.SetBool ("jump", false);//Jumping animation is deactivated
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground"){//################Important, the floor Tag must be "Ground" to detect the collision!!!!
			jumping=false;
			anim.SetBool ("jump", false);
		}

		if (coll.gameObject.tag == "Demon"){//################Important, the floor Tag must be "Ground" to detect the collision!!!!
			stopMoving = true;
			anim.SetBool ("walk", false);
		}
	}

	void Update () {
		counter = counter+Time.deltaTime;
		if (isdead == false) {
						//--DYING
						/*if(Input.GetKey ("k")){//###########Change the dead event, for example: life bar=0
				anim.SetBool ("dead", true);
				isdead=true;
			}*/
						//--END DYING

						//--JUMPING
						/*if (Input.GetButtonDown("Jump")){//Saltar
				if(jumping==false){//only once time each jump
					rigidbody2D.AddForce(new Vector2(0f,200));
					jumping=true;
					anim.SetBool ("jump", true);
				}
			}*/
						//--END JUMPING

						//--WALKING
						if (stopMoving == false) {
								float move = 0;
								if (counter < moveTime) {
										move = 1;
								} else if (counter > moveTime && counter < moveTime * 2) {
										move = -1;
								} else {
										counter = 0;
								}
								rigidbody2D.velocity = new Vector2 (move * maxspeed, rigidbody2D.velocity.y);
								if (move > 0) {//Go right
										anim.SetBool ("walk", true);//Walking animation is activated
										if (faceright == false) {
												Flip ();
										}
								}
								if (move == 0) {//Stop
										anim.SetBool ("walk", false);
								}			
								if ((move < 0)) {//Go left
										anim.SetBool ("walk", true);
										if (faceright == true) {
												Flip ();
										}
								}
								//END WALKING
						}
				}
	}

	void Flip(){
		faceright=!faceright;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
