using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBombExplosion : MonoBehaviour {
    public GameObject collisionEffectBlood;

    private void Start()
    {
        //DestroyExplosion();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Vector3 landingPos = transform.position;
        //landingPos.y += .5f;
        //GameObject spawnedParticlEffect = GameObject.Instantiate(collisionEffect, landingPos, Quaternion.identity);
        //spawnedParticlEffect.SetActive(true);
        //transform.gameObject.SetActive(false);

        if (other.gameObject.CompareTag("Enemy")) {
            Vector3 pos = other.gameObject.transform.position;
            pos.y += 1f;
            GameObject bloodEffect = GameObject.Instantiate(collisionEffectBlood, pos, Quaternion.identity);
            bloodEffect.SetActive(true);
        }

        Destroy(gameObject);
    }

    IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
