using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingItem : MonoBehaviour
{
    public float rotSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.parent.Rotate(new Vector3(0, rotSpeed, 0));
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Knife")
        {
            rotSpeed *= 1.2f;
        }
    }
}
