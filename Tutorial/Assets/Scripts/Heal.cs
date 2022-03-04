using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    
    
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            other.GetComponent<player>().isHealing = true;
            StartCoroutine(Healing(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            other.GetComponent<player>().isHealing = false;
        }
    }

    IEnumerator Healing(Collider col)
    {
        while (col.GetComponent<player>().isHealing && (col.GetComponent<player>().health < col.GetComponent<player>().maxHealth))
        {
            col.GetComponent<player>().health += 5;

            if (col.GetComponent<player>().health >= col.GetComponent<player>().maxHealth)
            {
                col.GetComponent<player>().health = col.GetComponent<player>().maxHealth;
            }
            yield return new WaitForSeconds(1);
        }

    }
}
