using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject explosion;

    private Rigidbody rigid;
    /*
    private float mass = 10;
    private float force = 200;
    private float acceleration;
    private float gravity = -9.81f;
    private float gAccel;
    private float speedZ;
    private float speedY;
    */
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "tank")
        {
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.forward = rigid.velocity;
        /*
        acceleration = force / mass;
        speedZ += acceleration * Time.deltaTime;
        
        gAccel = gravity / mass;
        speedY += gAccel * Time.deltaTime;
        
        transform.Translate(0, speedY, speedZ);
        
        force = 0;
        */
    }
    
}
