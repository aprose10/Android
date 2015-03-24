using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	Animator animator;
	public GameObject targetObject;
	bool updatingJumpCam = false;
	float changeValue = .2f;


	// Use this for initialization
	void Start () {
		animator = targetObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		float targetObjectX = targetObject.transform.position.x;
		float targetObjectY;
		if(animator.GetBool("jump")){

			//if hes falling underneath the camera
			if(targetObject.transform.position.y  + 2.6f < transform.position.y){
				if(targetObject.transform.position.y + 2.4f > transform.position.y - changeValue){
					//if the update is too far
					targetObjectY = targetObject.transform.position.y;
					updatingJumpCam = false;

				}else{
					//the update is not to far
					targetObjectY = transform.position.y - changeValue;
					updatingJumpCam = true;

				}
			}else{
				//dont move
				targetObjectY = transform.position.y;
			}
		}
		else{
			//if hes above the camera
			if(targetObject.transform.position.y + 2.4f > transform.position.y){
				targetObjectY = transform.position.y + 0.1f;


			}else{
				//check bool with if still jumping and wont move too far. Update it
				//if(updatingJumpCam){ print ("character " + targetObject.transform.position.y);
				//	print ("camera " + transform.position.y);}

				if(updatingJumpCam == true && targetObject.transform.position.y + 2.51f < transform.position.y - changeValue){
					targetObjectY = transform.position.y - changeValue;
				//	print ("character " + targetObject.transform.position.y);
			//		print ("camera " + transform.position.y);

				}else{
					targetObjectY = targetObject.transform.position.y + 2.5f;
					updatingJumpCam = false;
	
				}
			}
		}
		
		Vector3 newCameraPosition = transform.position;
		newCameraPosition.x = targetObjectX;
		newCameraPosition.y = targetObjectY;
		transform.position = newCameraPosition;
	}
}
