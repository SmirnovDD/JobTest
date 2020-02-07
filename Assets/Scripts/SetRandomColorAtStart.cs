using UnityEngine;

//дает прямоугольнику случайный цвет при добавлении в сцену
public class SetRandomColorAtStart : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1);
    }
}
