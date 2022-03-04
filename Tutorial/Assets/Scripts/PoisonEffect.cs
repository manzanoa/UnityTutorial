using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : MonoBehaviour
{

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            other.GetComponent<player>().isPoisoned = true;
            StartCoroutine(Poison(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            other.GetComponent<player>().isPoisoned = false;
        }
    }

    IEnumerator Poison(Collider col)
    {
        while (col.GetComponent<player>().isPoisoned && !col.GetComponent<player>().dead)
        {
            col.GetComponent<player>().health -= 5;

            if (col.GetComponent<player>().health <= 0)
            {
                col.GetComponent<player>().dead = true;
            }
            yield return new WaitForSeconds(1);
        }

    }
}
