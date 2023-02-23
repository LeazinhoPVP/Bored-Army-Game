using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public int speed;
    public ParticleSystem destroyEffect;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.right * Time.fixedDeltaTime * speed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {         
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Instantiate(destroyEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
