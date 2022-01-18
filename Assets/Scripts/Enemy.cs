using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(Random.Range(-9f, 9f),7,0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -5f){
            transform.position = new Vector3(Random.Range(-9f, 9f),7,0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.tag == "Player"){
           Player player = other.transform.GetComponent<Player>();
           if(player){
               player.Damage();
           }
           Destroy(this.gameObject);
       } else if(other.tag == "Laser"){
            //destroy laser
            //destroy laser
            Destroy(other.gameObject);
            Destroy(this.gameObject);

       }
    }
}
