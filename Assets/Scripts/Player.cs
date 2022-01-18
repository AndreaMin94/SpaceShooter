using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    private float _speedMultiplier = 2f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private float _nextFire = 0.0f;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _tripleShotActive = false;
    private bool _speedBostActive = false;
    

    // Start is called before the first frame update
    void Start()
    {
        //take the current position and assign as startin gposition 
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(!_spawnManager){
            Debug.LogError("Spawn Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();      
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire){  
            FireLaser();
        }
    }

    void Move(){

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if(_speedBostActive){
            transform.Translate(
                new Vector3(horizontalInput, verticalInput, 0)
                * (_speed * _speedMultiplier)
                * Time.deltaTime
            );
        } else {
                transform.Translate(
                new Vector3(horizontalInput, verticalInput, 0)
                * _speed
                * Time.deltaTime
            );
        }

        
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, -3.8f,0)
            ,0
        );

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -9, 9),
            transform.position.y,
            0
        );

    }

    void FireLaser(){
        _nextFire = Time.time + _fireRate;
        

        // instantiate triple shot prefab
        if(_tripleShotActive){
            Instantiate(
                _tripleShotPrefab, 
                transform.position,
                Quaternion.identity
        );
        } else {
            Instantiate(
                _laserPrefab,
                transform.position + new Vector3(0, 1.05f, 0),
                Quaternion.identity
        );
        }
    }

    public void Damage(){
        //if shield is, return
        _lives--;

        if(_lives < 1){
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void EnableTripleShot(){
        _tripleShotActive = true;
        StartCoroutine(DisableTripleShotRoutine());
    }

    IEnumerator DisableTripleShotRoutine(){
        yield return new WaitForSeconds(5.0f);
        _tripleShotActive = false;
    }

    public void SpeedBoostActive(){
        _speedBostActive = true;
        StartCoroutine(speedBoostPowerDownRoutine());
    }

    IEnumerator speedBoostPowerDownRoutine(){
        yield return new WaitForSeconds(5f);
        _speedBostActive = false;
    }

    // public void EnableSpeedBoost(){
    //     _speed = 10f;
    //     StartCoroutine(DisableSpeedBoosterRoutine());
    // }

    // IEnumerator DisableSpeedBoosterRoutine(){
    //     yield return new WaitForSeconds(10.0f);
    //     _speed = 5f;
    // }
}
