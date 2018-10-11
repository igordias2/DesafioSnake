using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Spawner_Controller : MonoBehaviour
{
    [Tooltip("Prefab da comida do player")]
    public GameObject foodPrefab;

    [Space]
    [Tooltip("Se estiver ativo o spawn vai ser por tempo ao inves de somente ao comer")]
    [SerializeField] bool timedSpawn;
    [Tooltip("Tempo para Spawnar")]
    [SerializeField] float timeToSpawn;
    float timerSpawn;

    [Space]
    [SerializeField] float cameraSizeHeight;
    [SerializeField] float cameraSizeWidth;

    [Space]
    [SerializeField] GameObject[] borderWidth;
    [SerializeField] GameObject[] borderHeight;

    [Space]
    [Tooltip("Todas as comidas que existem no jogo que estão na pasta /Resources/Food")]
    [SerializeField] List<Food> foods = new List<Food>();
    UnityEngine.Object[] foodsOB;


    private void Awake()
    {
        timerSpawn = timeToSpawn;
        GetCameraSize();
        GetAllFoods();
    }

    /// <summary>
    /// Pega todas as comidas que estão na pasta Resources/Food e adicionam a lista.
    /// </summary>
    private void GetAllFoods()
    {
        foodsOB = Resources.LoadAll("Food");
        foreach (Food f in foodsOB)
        {
            foods.Add(f);
        }
    }

    private void Start()
    {
        Spawn(foodPrefab);
    }

    /// <summary>
    /// Coloca os colliders no local da borda da camera.
    /// </summary>
    private void GetCameraSize()
    {
        cameraSizeHeight = Camera.main.orthographicSize;
        cameraSizeWidth = cameraSizeHeight * Camera.main.aspect;

        //Colocando os coliders nas paredes.
        foreach (GameObject bW in borderWidth)
        {
            bW.GetComponent<BoxCollider2D>().size = new Vector2(bW.GetComponent<BoxCollider2D>().size.x ,cameraSizeHeight * 2);
        }
        borderWidth[0].transform.position = new Vector2(-cameraSizeWidth, 0);
        borderWidth[1].transform.position = new Vector2(cameraSizeWidth, 0);
        foreach (GameObject bH in borderHeight)
        {
            bH.GetComponent<BoxCollider2D>().size = new Vector2(bH.GetComponent<BoxCollider2D>().size.x, cameraSizeWidth * 2);
        }
    }

    private void Update()
    {
        CheckTimed();
    }

    /// <summary>
    /// Checa se é pra spawnar com o tempo
    /// </summary>
    private void CheckTimed()
    {
        if (timedSpawn)
        {
            timerSpawn -= 1 * Time.deltaTime;
            if (timerSpawn <= 0)
            {
                Spawn(foodPrefab);
                timerSpawn = timeToSpawn;
            }
        }
    }

    /// <summary>
    /// Instancia o prefab aleatoriamente no mundo dentro da camera
    /// </summary>
    /// <param name="prefab">Prefab a ser instanciado</param>
    public void Spawn(GameObject prefab)
    {
        float rX = UnityEngine.Random.Range(-cameraSizeWidth, cameraSizeWidth);
        float rY = UnityEngine.Random.Range(-cameraSizeHeight, cameraSizeHeight);

        GameObject food = Instantiate(prefab, new Vector2(rX, rY), Quaternion.identity);
        food.GetComponent<Food_Controller>().SetFood(getFood());
    }
    Food getFood()
    {
        int rF = UnityEngine.Random.Range(0, foods.Count);
        return foods[rF];
    }
}
