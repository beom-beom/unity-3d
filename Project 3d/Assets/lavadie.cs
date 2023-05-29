using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavadie : MonoBehaviour
{
    private HealthSystemForDummies playerhealthSystem;

    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        playerhealthSystem = player.GetComponent<HealthSystemForDummies>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             playerhealthSystem.CurrentHealth = 0;
        }
    }
}