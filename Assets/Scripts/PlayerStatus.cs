using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerStatus : MonoBehaviour
{

    public int  nivel = 1;
    private bool isDead = false, isMaxedAttackSpeed = false;
    public TextMeshProUGUI playerHpText;
    public HealthBar healthBar;

    //Valores Base Privados
    private double baseStrength = 10, baseAgility = 10, baseIntelligence = 10, baseLucky = 1, baseHealth = 400, baseDamage = 20, baseAttackSpeed = 120;

    public double strength, totalStrength;
    public double agility, totalAgility;
    public double intelligence, totalIntelligence;
    public double lucky, totalLucky;
    
    public double health, totalHealth, actualHealth, healthRegeneration;
    public double extraDamage, totalDamage, criticalChance, criticalMultiplier = 150;

    public double  attackSpeed, totalAttackSpeed;

    public float timeHealthRegen = 1, shootForce;

    //Valor de Tempo pro projetil ficar ativo
    //public float timeBulletActive;

    public double xpTotal;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("RegenHealth", 1f, timeHealthRegen);
        UpdateStats();
        isDead = false;
        actualHealth = totalHealth;
        healthBar.SetHealthBarMaxValue(totalHealth);
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStats();
        playerHpText.text = "+" + actualHealth;

        if(isDead)
        {

            Debug.Log("Voce morreu!");
            //Respawn();
        }
    }



    public void ReceiveXp(double experience)
    {
        if(nivel < 30){
            xpTotal += experience;
            CheckLevel();
        }
    }

    //Funcao para checar o nivel e upar
    public void CheckLevel()
    {
        float[] level = {0, 230, 600, 1080, 1660, 2260, 2980, 3730, 4620, 5550, 6520, 7530, 8580, 9805, 11055, 12330, 13630, 14955, 16455, 18045, 19645, 21495, 23595, 25945, 28545, 32045, 36545, 42045, 48545, 56045};
        while(xpTotal >= level[nivel]){
            
            nivel++;
            strength += 3;
            intelligence += 2;
            agility += 1.5;
            lucky += 0.5;
            if(isMaxedAttackSpeed)
            {
                addAttackSpeed(agility);
            }
            

            UpdateStats();
            actualHealth = totalHealth;

            if(nivel == 30)
            {
                break;
            }
           
        }
    }

    //Funcao que atualiza os status do personagem
    public void UpdateStats()
    {



        //Update Status
        totalStrength = baseStrength + strength;
        totalIntelligence = baseIntelligence + intelligence;
        totalAgility = baseAgility + agility;
        totalLucky = baseLucky + lucky;



        //Update Critical chance-
        criticalChance = totalLucky * 0.5;

        //Update Projetil Force
        shootForce = (float) (totalIntelligence * 0.5);

        //Update Atk Speed
        attackSpeed = totalAgility;
        totalAttackSpeed = attackSpeed + baseAttackSpeed;

        //Update Damage
        extraDamage = totalAgility * 1;
        totalDamage = baseDamage + extraDamage;

        //Update Health/Regen
        healthRegeneration = totalStrength * 0.2;
        health = totalStrength * 10;
        totalHealth = baseHealth + health;
    }

    //Funcao que recebe Dano
    public void TakeDamage(double damage)
    {
        actualHealth -= damage;
        healthBar.SetHealthBar(actualHealth);
        if(actualHealth <= 0)
        {
            isDead = true;
        }
    }

    //Funcao que retorna o dano
    public double getDamage(){
        int chance = Random.Range(0, 101);
        if(criticalChance * 100 >= chance)
        {
            return totalDamage * criticalMultiplier;
        }
        return totalDamage;
    }

    //Funcao de regenerar vida
    public void RegenHealth(){

        if((actualHealth += healthRegeneration) > totalHealth)
        {
            actualHealth = totalHealth;
        }
    }

    public double getAttackSpeed(){
        return attackSpeed;
    }

    public float getShootForce(){
        return shootForce;
    }

    //Adiciona Ataque Speed
    public void addAttackSpeed(double extra)
    {
        if(totalAttackSpeed >= 1000){
            totalAttackSpeed = 1000;
            isMaxedAttackSpeed = true;
        }
        else
            attackSpeed += extra;
            totalAttackSpeed = attackSpeed + baseAttackSpeed;
    }

    

}
