using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Food_Controller : MonoBehaviour
{
    [Header("a Food atual da instancia")]
    [SerializeField] Food food;

    int score;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (this.gameObject.GetComponent<SpriteRenderer>())
            spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Define os parametros da comida secionada na instancia
    /// </summary>
    /// <param name="f">Comida selecionada</param>
    public void SetFood(Food f)
    {
        this.score = f.score;
        this.spriteRenderer.color = f.color;
    }

    /// <summary>
    /// Retorna o Score da Instancia
    /// </summary>
    /// <returns></returns>
    public int GetScore()
    {
        return score;
    }

}
