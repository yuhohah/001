using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    //
    public HealthBar healthBar;
    //Local de ataque
    public Transform attackPoint; 

    //Variavel de controle de Status do player -- Onde recebe XP e Toma Dano
    private PlayerStatus playerStatus;

    //Variaveis do Inimigo
    public double actualHealth, totalHealth = 50;
    public double experience, damage = 10f;

    //Variavel da IA
    public NavMeshAgent agent;

    public Transform player;

    //Reconhecer esses objetos
    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walPoint;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        playerStatus = GameObject.FindObjectOfType<PlayerStatus>();
        player = GameObject.Find("Personagem").transform;
        agent = GetComponent<NavMeshAgent>();
        actualHealth = totalHealth;
        healthBar.SetHealthBarMaxValue(totalHealth);
    }

    void Update()
    {
        //Check for sight and attack
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy dont move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            //Attack code here 
            Attack();
            //
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        //Condicao para atackar novamente -- Atualmente apenas a cada intervalo
        alreadyAttacked = false;
    }

    public void TakeDamage(double damage)
    {
        actualHealth -= damage;
        healthBar.SetHealthBar(actualHealth);
        if (actualHealth <= 0f)
        {
            playerStatus = GameObject.FindObjectOfType<PlayerStatus>();
            playerStatus.ReceiveXp(experience);
            Die();
        }
    }

    void Die(){

        //Comecar animacao de Morte
        Destroy(gameObject);
    }

    void Attack()
    {
        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRange, whatIsPlayer);

        foreach(Collider player in hitPlayers){
            playerStatus.TakeDamage(damage);
            Debug.Log("Attacando!" + player.name);
        }
    }

    private void OnDrawGizmos()
    {
        if(attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
