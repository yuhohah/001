using UnityEngine;

public class Staff : MonoBehaviour
{
    public float damage = 10f;
    public float range = 10f;
    public float fireRate = 10f;

    public Camera fpsCam;

    private float nexTimeToFire = 0f;

    //public ParticleSystem muzzleFlash;
    //public GameObject impactEffect;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nexTimeToFire)
        {
            nexTimeToFire = Time.time + 1f/fireRate;
            Shoot();
        }
    }
    void Shoot()
    {
        //PLay na animacao de Atirar.
        //muzzleFlash.Play();

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Enemy target = hit.transform.GetComponent<Enemy>();

            if(target != null)
            {
                target.TakeDamage(damage);
            }

            //GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            //Destroy(impact, 2f);
        }

    }
}
