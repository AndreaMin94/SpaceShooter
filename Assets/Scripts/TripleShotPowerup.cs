using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotPowerup : MonoBehaviour
{
    private float _speed = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(Random.Range(-9f, 9f), 7, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -4f){
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            Player player = other.transform.GetComponent<Player>();
            if(player){
                player.EnableTripleShot();
            }
            Destroy(this.gameObject);
        }
    }
}
