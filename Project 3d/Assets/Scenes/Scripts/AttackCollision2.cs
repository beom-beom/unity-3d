using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision2 : MonoBehaviour
{

    private void OnEnable()
    {
        StartCoroutine("AutoDisable");
    }
    private void OnTriggerEnter(Collider other)
    {
        Dragoncontroller dragon = other.GetComponent<Dragoncontroller>();
        MummyController mummy = other.GetComponent<MummyController>();
        MushControllor mush = other.GetComponent<MushControllor>();
        if (dragon != null)
        {
            dragon.TakeDamage(10, transform.forward);
        }
        if (mummy != null)
        {
            mummy.TakeDamage(10, transform.forward);
        }
        if (mush != null)
        {
            mush.TakeDamage(10, transform.forward);
        }
    }
    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }
}
