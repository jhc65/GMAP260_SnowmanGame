using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowmanHealth : MonoBehaviour {
    public int health = 3;
	public float invincibilityTime = 3f;
	public Text gameOverText;
	public Text bunnyCountText;

    public GameObject head;
    public GameObject middle;
    public GameObject bottom;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject shotSpawner;

	private Transform snowman;
	private float timeSinceLastHit;
	private int bunniesKilled = 0;

	// Use this for initialization
    void Start () {
		snowman = gameObject.GetComponentInParent<Transform>();
		timeSinceLastHit = invincibilityTime;
    }

    // Update is called once per frame
    void Update()
    {
		timeSinceLastHit += Time.deltaTime;
    }

	public void IncrementBunniesKilled() {
		bunniesKilled++;
	}

    private void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Enemy")
        {

			if (timeSinceLastHit <= invincibilityTime)
				return;
			float height = 1f;

            health--;
			timeSinceLastHit = 0;

			// Lose bottom. Only torso and head
            if (health == 2)
            {

                bottom.SetActive(false);
				head.transform.position = new Vector3(head.transform.position.x, head.transform.position.y - height, head.transform.position.z);
				middle.transform.position = new Vector3(middle.transform.position.x, middle.transform.position.y - height, middle.transform.position.z);
				leftHand.transform.position = new Vector3(leftHand.transform.position.x, leftHand.transform.position.y - height, leftHand.transform.position.z);
				rightHand.transform.position = new Vector3(rightHand.transform.position.x, rightHand.transform.position.y - height, rightHand.transform.position.z);
				return;
			}

			// Lose torso. Only head. 
            else if (health == 1)
            {
                middle.SetActive(false);
                leftHand.SetActive(false);
                rightHand.SetActive(false);
				head.transform.position = new Vector3(head.transform.position.x, head.transform.position.y - height, head.transform.position.z);
				shotSpawner.transform.position = new Vector3(head.transform.position.x + .5f, head.transform.position.y + 1f, head.transform.position.z + 2f);
				return;
            }
            if (health == 0)
            {

                head.SetActive(false);
				gameOverText.enabled = true;
				bunnyCountText.text = bunniesKilled.ToString() + " Bunnies Killed";
				bunnyCountText.enabled = true;

				gameObject.GetComponentInParent<PlayerController>().DisableMovementAndShooting();
				return;
			}

        }
    }
}
