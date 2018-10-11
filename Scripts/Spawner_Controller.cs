using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Controller : MonoBehaviour
{
    [Tooltip("Prefab da comida do player")]
    public GameObject foodPrefab;

    [Tooltip("Se estiver ativo o spawn vai ser por tempo ao inves de somente ao comer")]
    [SerializeField] bool timedSpawn;
    [Tooltip("Tempo para Spawnar")]
    [SerializeField] float timeToSpawn;
    float timerSpawn;

    [SerializeField] float cameraSizeHeight;
    [SerializeField] float cameraSizeWidth;


    private void Awake()
    {
        timerSpawn = timeToSpawn;
        GetCameraSize();
    }
    private void Start()
    {
        Spawn(foodPrefab);
    }

    private void GetCameraSize()
    {
        cameraSizeHeight = Camera.main.orthographicSize;
        cameraSizeWidth = cameraSizeHeight * Camera.main.aspect;
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

        Instantiate(prefab, new Vector2(rX, rY), Quaternion.identity);
    }
}
