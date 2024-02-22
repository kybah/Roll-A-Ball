using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{  
    public float speed = 1f;
    private Rigidbody rb;
    public int pickUpCount; 

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
        //Get the number of pick ups in our scene
        pickUpCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        //Run the check pick ups functon
        CheckPickUps();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movehorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(movehorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pick Up")
        {
            Destroy(other.gameObject);
            pickUpCount--;
            CheckPickUps();
        }
        


    }

 void CheckPickUps()
    {
        print("Pick Ups Left: " + pickUpCount);
        if(pickUpCount == 0)
        {
            print("congratulations");
        }
    }
}
