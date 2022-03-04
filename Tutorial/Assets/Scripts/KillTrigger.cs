using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            other.gameObject.GetComponent<player>().dead = true;
        }
    }
}
