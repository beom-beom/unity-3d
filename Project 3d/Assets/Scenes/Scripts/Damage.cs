using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null && player.gameObject.layer == 3)
        {
            player.TakeDamage(10, transform.forward);
        }
    }
}
