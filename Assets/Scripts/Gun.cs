using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Reference
    public Transform attackPoint;
    public Camera fpsCam;

    //Player Status
    private PlayerStatus playerStatus;

    //Bullet Object
    public GameObject bulletPrefab;

    //Valores
    private double fireRate, baseAttackTime = 1.5;
    private double nexTimeToFire = 0f;


    void Update(){

        playerStatus = GameObject.FindObjectOfType<PlayerStatus>();

        fireRate = 1 + playerStatus.getAttackSpeed() * 0.01;

        if(Input.GetButton("Fire1") && Time.time >= nexTimeToFire)
        {
            nexTimeToFire = Time.time + baseAttackTime/fireRate;
            Shoot();
        }
    }

    //Funcao para Atirar
    void Shoot()
    {
        //Encontra a posicao exata usando o RayCast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //Checa se acertou algo
        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);//Apenas um ponto longe do jogador
        }

        //Calcular a direcao do AttackPoint pro targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Cria a bullet/ projetil - Sem rotation
        GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, Quaternion.identity);
        
        //Rotaciona o projetil na direcao
        bullet.transform.forward = directionWithoutSpread.normalized;

        //Adiciona Forca ao projetil
        bullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread. normalized * playerStatus.getShootForce(), ForceMode.Impulse);
    }
}