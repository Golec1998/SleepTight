using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableCollisionScript : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        // Debug-draw all contact points and normals
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }

        // Play a sound if the colliding objects had a big impact.
        if (collision.relativeVelocity.magnitude > 2f)
            Debug.Log("bonk");
    }
}
