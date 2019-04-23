using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    public GameObject player;

    private float timer;
    private bool isBoosted;
    private PlayerController playerController;
    private Color tmp;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();

        tmp = GetComponent<SpriteRenderer>().color;

        isBoosted = false;

        timer = 10f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isBoosted = true;

            tmp.a = 0f;

            GetComponent<SpriteRenderer>().color = tmp;

            GetComponent<SphereCollider>().enabled = false;
        }

    }

    void Update ()
    {
        if (isBoosted)
            if (timer > 0)
            {
                timer -= Time.deltaTime;

                playerController.speed = 15f;
            }
                else 
            {
                playerController.speed = 10f;

                isBoosted = false;
            }
    }
}
