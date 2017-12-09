using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallCollision : MonoBehaviour {
    public GameObject collisionEffect;
    public GameObject collisionEffectBlood;
    public GameObject explosion;

    // get audio src snowball-impact-snow.wav
    private AudioSource audio;
    public AudioClip snowballImpactGroundSound;

    void OnCollisionEnter(Collision collision)
    {
        Vector3 landingPos = transform.position;
        landingPos.y += .5f;
        GameObject spawnedParticlEffect = GameObject.Instantiate(collisionEffect, landingPos, Quaternion.identity);
        spawnedParticlEffect.SetActive(true);
        transform.gameObject.SetActive(false);

        if (collision.gameObject.CompareTag("Ground")) {
            PlaySound(snowballImpactGroundSound, transform.position);
			Debug.Log ("ground");
        }
        if (collision.gameObject.CompareTag("Enemy")) {
            Vector3 pos = collision.gameObject.transform.position;
            pos.y += 1f;
            GameObject bloodEffect = GameObject.Instantiate(collisionEffectBlood, pos, Quaternion.identity);
            bloodEffect.SetActive(true);
        }

        GameObject explosionObject = GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
        explosionObject.SetActive(true);
        Destroy(gameObject);

        //}// else if(collision.gameObject.tag == "Tree")
        // play snowball hit tree sound
    }

    void PlaySound(AudioClip clip, Vector3 location)
    {
        AudioSource.PlayClipAtPoint(clip, location);
    }
}
