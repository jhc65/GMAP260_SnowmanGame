using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBallCollision : MonoBehaviour {
    public GameObject collisionEffect;
    public GameObject collisionEffectBlood;

    // make snowball grow before it hits something
    private bool snowballShouldLerp = false;
    private Vector3 startTransform;
    private Vector3 endTransform;
    private float growthTimeInSeconds = 0.15f;
    private float totalLiveTimeInSeconds = 7.0f;
    private float endTime;
    private float startTime;

    //public float collisionShrinkFactor = 2f;

    //private Vector3 initialScale;
    //private float slowDownOnImpact = 3f;

    // get audio src snowball-impact-snow.wav
    private AudioSource audio;
    public AudioClip snowballImpactGroundSound;


    // Use this for initialization
    void Start()
    {
        endTime = (Time.time + totalLiveTimeInSeconds);
        //initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= endTime) {
            Destroy(gameObject);
        }
        if (snowballShouldLerp) {
            transform.localScale = Vector3.Lerp(startTransform, endTransform, (Time.time - startTime));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 landingPos = transform.position;
        landingPos.y += .5f;
        GameObject spawnedParticlEffect = GameObject.Instantiate(collisionEffect, landingPos, Quaternion.identity);
        spawnedParticlEffect.SetActive(true);

        if (collision.gameObject.CompareTag("Ground")) {
            PlaySound(snowballImpactGroundSound, transform.position);
            InvokeRepeating("SetTransforms", 0.0f, growthTimeInSeconds);
            snowballShouldLerp = true;
        }
        if (collision.gameObject.CompareTag("Enemy")) {
            CancelInvoke("SetTransforms");
            Vector3 pos = collision.gameObject.transform.position;
            pos.y += 1f;
            GameObject bloodEffect = GameObject.Instantiate(collisionEffectBlood, pos, Quaternion.identity);
            bloodEffect.SetActive(true);
        }

        //}// else if(collision.gameObject.tag == "Tree")
        // play snowball hit tree sound
    }

    void SetTransforms()
    {
        startTime = Time.time;
        startTransform = transform.localScale;
        endTransform = new Vector3(startTransform.x + 0.4f, startTransform.y + 0.4f, startTransform.z + 0.4f);
    }


    void PlaySound(AudioClip clip, Vector3 location)
    {
        AudioSource.PlayClipAtPoint(clip, location);
    }
}
