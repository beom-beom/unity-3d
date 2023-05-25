using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{

    private void OnEnable()
    {
        StartCoroutine("AutoDisable");
    }
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            player.TakeDamage(10, transform.forward);
        }
    }
    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(0.1f);
         gameObject.SetActive(false);
    }
}
