using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    [Header("Waypoint Settings")]
    //Atributos para controle de Waypoint
    public GameObject[] waypoints;
    int currentWP = 0;

    [Header("Movement Settings")]
    //Atributos para controle do Movimento do objeto
    public float speed = 1.0f;
    float accuracy = 1.0f;
    float rotSpeed = 0.4f;


    void Start()
    {
        //Get waypoints in Scene - aqui eh inserido dentro do array waypoints um array criado pela unity com todos os gameobjects com a tag waypoint na cena
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }

    void LateUpdate()
    {
        //Caso nao tenha nenhum waypoint na cena nao executa o resto do script
        if (waypoints.Length == 0) return;

        //Cria um Vector3 utilizando o X do waypoint atual, o Y deste objeto e o Z do waypoint atual
        Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x,
            this.transform.position.y,
            waypoints[currentWP].transform.position.z);

        //Gira o objeto para que ele olhe para o waypoint atual e define o valor direction como a distancia entre o objeto e o waypoint atual
        Vector3 direction = lookAtGoal - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
            Quaternion.LookRotation(direction),
            Time.deltaTime * rotSpeed);

        //Se o objeto estiver proximo do waypoint atual
        if (direction.magnitude < accuracy)
        {
            //Define o waypoint atual como o próximo do array
            currentWP++;
            //Se não tiver mais waypoints no array
            if (currentWP >= waypoints.Length)
            {
                //Define o waypoint atual como o primeiro (0)
                currentWP = 0;
            }
        }
        /*Move o objeto no eixo Z, basicamente faz o objeto se mover na cena até o waypoint 
         * (mas ele soh faz isso pq lah em cima ele faz o objeto sempre estar olhando o waypoint)*/
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
