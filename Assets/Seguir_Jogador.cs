using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seguir_Jogador : MonoBehaviour
{
    public Player_Controller pc;
    public Vector3 distanciaDoJogador;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = pc.transform.position + distanciaDoJogador;
    }
}
