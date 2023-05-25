using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Dragoncontroller dragon = other.GetComponent<Dragoncontroller>();
        MummyController mummy = other.GetComponent<MummyController>();
        MushControllor mush = other.GetComponent<MushControllor>();
        if (dragon != null)
        {
            dragon.TakeDamage(5, transform.forward);
        }
        if (mummy != null)
        {
            mummy.TakeDamage(5, transform.forward);
        }
        if (mush != null)
        {
            mush.TakeDamage(5, transform.forward);
        }
    }
}
