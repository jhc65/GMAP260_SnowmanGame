using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanHealth : MonoBehaviour {
    private int health = 3;
    public GameObject head;
    public GameObject middle;
    public GameObject bottom;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject shotSpawner;
    private EnemySpawner enemySpanwer;
    // Use this for initialization
    void Start () {
        health = 3;
        GameObject enemySpanwerObject = GameObject.FindWithTag("Respawn");
        if (enemySpanwerObject != null)
        {
            enemySpanwer = enemySpanwerObject.GetComponent<EnemySpawner>();

        }
        else
        {
            Debug.Log("cannot find 'EnemySpawner'");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            health--;
            Debug.Log("Hello", gameObject);
            if (health == 2)
            {
                bottom.SetActive(false);
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - bottom.transform.lossyScale.y, gameObject.transform.position.z);
            }

            else if (health == 1)
            {
                middle.SetActive(false);
                leftHand.SetActive(false);
                rightHand.SetActive(false);
                shotSpawner.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + middle.transform.lossyScale.y, gameObject.transform.position.z);
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - middle.transform.lossyScale.y, gameObject.transform.position.z);
                
            }
            if (health == 0)
            {
                head.SetActive(false);

                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - head.transform.lossyScale.y, gameObject.transform.position.z);
               
                
            }
        }
    }
}
