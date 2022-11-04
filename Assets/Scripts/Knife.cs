using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public Vector3 throwForce;
    public Rigidbody rb;
    public BoxCollider bCollider;
    private bool isActive = true;
    public float gravityScale = 1.0f;
    public GameController gameController;

   void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && isActive)
        {
            rb.AddForce(throwForce, ForceMode.Impulse);
            GameController.Instance.GameUI.DecrementDisplayedKnifeCount();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isActive)
            return;
        isActive = false;

        if (collision.collider.tag == "Pizza")
        {
            GetComponent<ParticleSystem>().Play();

            rb.velocity = new Vector3(0, 0, 0);
            rb.isKinematic = true;
            transform.SetParent(collision.collider.transform);

            bCollider.center = new Vector3(-0.2f, 0.01f, bCollider.center.z);
            bCollider.size = new Vector3(0.4f, bCollider.size.y, 0.12f);

            GameController.Instance.OnSuccessfulKnifeHit();
        } 
        else if(collision.collider.tag == "Knife")
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -99);
            GameController.Instance.StartGameOverSequence(false);
        }
    }

  
}
