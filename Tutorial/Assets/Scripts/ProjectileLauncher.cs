using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectile;
    public GameObject p;
    public float launchVelocity = 700f;
    public Transform prSpawn;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && p.GetComponent<player>().ammo > 0)
        {
            GameObject ball = Instantiate(projectile, transform.position, transform.rotation);
            ball.GetComponent<Rigidbody>().velocity = prSpawn.TransformDirection(Vector3.forward * 200);
            p.GetComponent<player>().ammo--;
            p.GetComponent<player>().ammoText.text = p.GetComponent<player>().ammo.ToString();
        }
    }
}
