using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    int quantAtual = 0;
    int quantAnterior = 1;

    float velocidade = 50;

    public Transform[] spawnPoints;

    float timer = 0f;
    float timer1 = 0f;


    public GameObject nave;
    public static Game SharedInstance;
    public List<GameObject> pooledObjects;
    public int amountToPool;

    int numNave;
    public Text numNaves;

    private void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < amountToPool; i++)
        {
            Add();
        }

        numNaves = numNaves.GetComponent<Text>();

    }
    int soma;

    private GameObject Add()
    {
        GameObject tmp;
        tmp = Instantiate(nave);
        tmp.SetActive(false);
        pooledObjects.Add(tmp);
        return tmp;
    }

    void Update()
    {
        if (timer <= 1)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            int navesIndex = Random.Range(0, pooledObjects.Count);
            pooledObjects[navesIndex].SetActive(false);
        }

        if (timer1 <= 2)
        {
            timer1 += Time.deltaTime;
        }
        else
        {
            timer1 = 0;
            soma = quantAtual + quantAnterior;
            quantAnterior = quantAtual;
            quantAtual = soma;
            index = 0;
            Spaw();
        }
    }
    int index;

    private void Spaw()
    {
        index++;
        if (index < soma)
        {
            LauchShip();
            Spaw();
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        
        return Add();
    }

    void LauchShip()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        GameObject nave = SharedInstance.GetPooledObject();
        if (nave != null)
        {
            nave.transform.position = spawnPoints[spawnPointIndex].position;
            nave.transform.rotation = spawnPoints[spawnPointIndex].rotation;
            nave.SetActive(true);
            velocidade = Random.Range(5, 10);
            nave.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, velocidade);
        }

        numNave++;
        numNaves.text = numNave + " Naves Criadas";
    }
}
