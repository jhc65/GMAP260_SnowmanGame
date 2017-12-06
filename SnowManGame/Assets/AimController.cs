using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour {

	public GameObject target; // Crosshair sprite object

	public float minZOffset = 2; // How close the crosshairs can be to player
	public float maxZOffset = 40; // How far the crosshairs can be to player

	public float maxDownShootingOffset = 40f;
	public float maxUpShootingOffset = 320f;


	public float crosshairSensitivity = 3; // Crosshair movement sensitivity. sorta a hardcoded value that takes adjusting. don't touch
	public float aimingAngleSensitivty = 1;
	private GameObject shotSpawner;

	// Use this for initialization
	void Start () {
		shotSpawner = this.gameObject;
		target.transform.position = new Vector3(shotSpawner.transform.position.x, 0.5f, shotSpawner.transform.position.z + 5f);
	}

	// Update is called once per frame
	void Update () {
		float mouseY = -Input.GetAxis("Mouse Y");
		float thetaChange = mouseY * aimingAngleSensitivty * Time.deltaTime;
		float x,y,z;
		z = 0f;
		x = transform.eulerAngles.x;
		y = transform.parent.eulerAngles.y;
		y = transform.parent.eulerAngles.y;
		float newTheta = x + thetaChange;
		Vector3 desiredRotation = new Vector3(newTheta, y, z);

		// Aiming up yields a desiredRotation.x of 360, 359, 358...
		// Aiming down yields a desiredRotation.x of 0, 1, 2 ...
		// Cap these values to avoid a free rotation, recommended at 40 and 360 min and max
		if (desiredRotation.x > maxDownShootingOffset && desiredRotation.x < maxDownShootingOffset * 2)
			desiredRotation = new Vector3(maxDownShootingOffset, y, z); // lock down shooting
		else if (desiredRotation.x < maxUpShootingOffset && desiredRotation.x > maxDownShootingOffset * 2)
			desiredRotation = new Vector3(maxUpShootingOffset, y, z); // lock up shooting
		transform.rotation = Quaternion.Euler(desiredRotation);

		// Estimate launched distance
		float v = GetComponent<ShootController>().GetBulletLaunchSpeed();
		float g = Physics.gravity.y;
		float height = transform.position.y;
		float theta = transform.eulerAngles.x - 360;
		float newZ = CalculateDistanceToTarget(v, g, height, theta);
		newZ = newZ / crosshairSensitivity;
		target.transform.localPosition = new Vector3(target.transform.localPosition.x, .02f, newZ);
	}

	// Calculate the distance a projectile should travel launched at an angle
	private float CalculateDistanceToTarget(float v, float g, float y, float theta) {
		g = Mathf.Abs(g);
		theta *= -1;
		theta = theta * Mathf.Deg2Rad;

		// Use classic kinematics to irst calculate time,
		// then calculate time * horiz vel to get distance
		float b = v * Mathf.Sin(theta);
		float t1 = (b + Mathf.Sqrt(b * b - 4 * (g / 2) * (-1 * y))) / g;
		float t2 = (b + Mathf.Sqrt(b * b - 4 * (g / 2) * (-1 * y))) / g;
		float time = t1 > 0 ? t1 : t2;
		return v * Mathf.Cos(theta) * time;
	}

}
