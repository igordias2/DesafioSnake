using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    [Tooltip("Prefab do Corpo")]
    [SerializeField] GameObject bodyPrefab;
    [Tooltip("Onde os bodies vão ficar como Parent")]
    [SerializeField] GameObject playerBody;

    [Space]
    [Tooltip("Corpos da cobra")]
    [SerializeField] List<GameObject> bodies = new List<GameObject>();

    [Space]
    [Tooltip("A direção que irá se mover")]
    [SerializeField] int movDirection;


    [Space]
    [Tooltip("Tamanho a mover")]
    [SerializeField] float size;


    [Space]
    [Tooltip("Velocidade com que se chama o update de movimento, quanto menor, mais rapido.")]
    [SerializeField] float movSpeed;
    float callTimes;

    [Space]
    [SerializeField] int score = 0;
    [SerializeField] TMPro.TMP_Text scoreText;

    //Ultima posição do player
    Vector2 lastPos;

    Spawner_Controller spawnerController;

    private void Start()
    {
        spawnerController = GameObject.FindObjectOfType<Spawner_Controller>();
        callTimes = movSpeed;
    }
    void Update()
    {
        CheckMove();
    }
    private void CheckMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            if (horizontal > vertical)
            {
                if (horizontal > 0)
                {
                    if(movDirection != 4)
                        ChangeDir(3);
                }
                else
                {
                    if (movDirection != 1)
                        ChangeDir(2);
                }

            }
            else
            {
                if (vertical > 0)
                {
                    if (movDirection != 2)
                        ChangeDir(1);
                }
                else
                {
                    if (movDirection != 3)
                        ChangeDir(4);
                }
            }
        }

        callTimes -= 1 * Time.deltaTime;
        if (callTimes <= 0)
        {
            Move();
            callTimes = movSpeed;
        }
    }

    /// <summary>
    /// Muda a direção que a cobra está indo
    /// </summary>
    /// <param name="d">direção</param>
    private void ChangeDir(int d)
    {
        movDirection = d;
    }

    /// <summary>
    /// Update de Movimento.
    /// </summary>
    private void Move()
    {
        lastPos = this.transform.position;
        switch (movDirection)
        {
            //Cima
            case 1: this.transform.position = lastPos + new Vector2(0, size)* Time.deltaTime; break;
            //Baixo
            case 2: this.transform.position = lastPos + new Vector2(0, -size) * Time.deltaTime; break;
            //Esquerda
            case 3: this.transform.position = lastPos + new Vector2(size, 0) * Time.deltaTime; break;
            //Direita
            case 4: this.transform.position = lastPos + new Vector2(-size, 0) * Time.deltaTime; break;
                ////Cima
                //case 1: this.transform.Translate(new Vector2(0, size) * movSpeed * Time.deltaTime); break;
                ////Baixo
                //case 2: this.transform.Translate(new Vector2(0, -size) * movSpeed * Time.deltaTime); break;
                ////Esquerda
                //case 3: this.transform.Translate(new Vector2(size, 0) * movSpeed * Time.deltaTime); break;
                ////Direita
                //case 4: this.transform.Translate(new Vector2(-size, 0) * movSpeed * Time.deltaTime); break;
        }
        CheckBodies();
    }

    /// <summary>
    /// Faz a checagem para que o ultimo vá para o lugar da ultima posição da "cabeça"
    /// </summary>
    private void CheckBodies()
    {
        if (bodies.Count > 0)
        {
            bodies.Last().transform.position = lastPos;
            bodies.Insert(0, bodies.Last());
            bodies.RemoveAt(bodies.Count - 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            AddBody(collision.gameObject);
        }
        if (collision.CompareTag("Wall"))
        {
            GameOver(collision.gameObject);
        }
        if (collision.CompareTag("Body"))
        {
            foreach (GameObject body in bodies)
            {
                if(body == collision.gameObject)
                    GameOver(collision.gameObject);
            }
        }
    }
    /// <summary>
    /// Adiciona um pedaço do corpo
    /// </summary>
    /// <param name="go">go é o GameObject que se vai tirar os parametros</param>
    void AddBody(GameObject go)
    {
        
        GameObject newBody = Instantiate(bodyPrefab, lastPos, Quaternion.identity);
        newBody.transform.SetParent(playerBody.transform);
        bodies.Add(newBody);

        SetScore(go.GetComponent<Food_Controller>().GetScore());
        Destroy(go);

        spawnerController.Spawn(spawnerController.foodPrefab);
    }

    /// <summary>
    /// Mudar o score
    /// </summary>
    /// <param name="scoreP">pontuacao para adicionar ao score</param>
    void SetScore(int scoreP)
    {
        score += scoreP;
        scoreText.text = score.ToString();
    }
    /// <summary>
    /// Chama o game over
    /// </summary>
    /// <param name="go">o Obj que lhe matou</param>
    void GameOver(GameObject go)
    {
        GameObject.FindObjectOfType<GameOver_Controller>().GameOver(score);
    }
}
