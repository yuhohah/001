using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public PlayerStatus player;

    public float timeBulletActive;
    private float timeSpawn;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerStatus>();
        timeSpawn = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Time.time >= timeSpawn + timeBulletActive)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(player.getDamage());
            Destroy(gameObject);
        }
        
    }
}
