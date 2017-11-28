using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;       //Public variable to store a reference to the player game object

	public Vector3 OffsetFromPlayer;
	public float verticalSpeed = 2f;

	private Vector3 offset;     
	public bool lookAt = true;
	public Transform target;
	public Space offsetPositionSpace = Space.Self;

	private float pitch = -10f;
	private float clampedPitch = -10f;
	private float verticalAdjustmentFactor; // Move camera up and down to see farther by the amount mouse Y * speed / this factor)

	void Start () {
		// Disable cursor
		Cursor.visible = false;

		//Calculate and store the offset value by getting the distance between the player's position and camera's position.
		offset = transform.position - player.transform.position;
		offset += OffsetFromPlayer;
		verticalAdjustmentFactor = verticalSpeed * 10; 
	}

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{

		// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
		if (offsetPositionSpace == Space.Self) {
			transform.position = target.TransformPoint(offset);
		}
		else {
			transform.position = target.position + offset;
		}

		// compute rotation
		if (lookAt) {
			transform.LookAt(target);


			// Copy rotation of player x but rotate y with Mouse Y
			pitch -= verticalSpeed * Input.GetAxis("Mouse Y");
			clampedPitch = Mathf.Clamp(pitch, -40, 90);
			transform.localEulerAngles = new Vector3(clampedPitch, transform.localEulerAngles.y, 0f);

			// zoom out effect with pitch (go up as angle is negative)
			transform.position = new Vector3(transform.position.x, transform.position.y + clampedPitch / verticalAdjustmentFactor, transform.position.z);
			
		}
		else {
		//	transform.rotation = target.rotation;
		}
	}
}

