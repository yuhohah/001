using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public Rigidbody rb;
    public int velocidadeDeLado;
    public int velocidadeDeFrente;
    public int tamanhoPulo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("a") == true){
            Debug.Log("A apertado");
            rb.AddForce(-velocidadeDeLado * Time.deltaTime, 0 , 0);
        }

        if(Input.GetKey("d") == true){
            Debug.Log("D apertado");
            rb.AddForce(velocidadeDeLado * Time.deltaTime, 0 , 0);
        }

        if(Input.GetKey("s") == true){
            Debug.Log("S apertado");
            rb.AddForce(0, 0 , -velocidadeDeFrente * Time.deltaTime);
        }

        if(Input.GetKey("w") == true){
            Debug.Log("W apertado");
            rb.AddForce(0, 0 , velocidadeDeFrente * Time.deltaTime);
        }

        if(Input.GetKey("space") == true){
            Debug.Log("Espaco apertado");
            rb.AddForce(0, tamanhoPulo * Time.deltaTime, 0);
        }
        
    }
}
