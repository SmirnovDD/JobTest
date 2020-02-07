using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//добавляет и удаляет прямоугольники, а также создает между ними связь
public class RectangleManager : MonoBehaviour
{
    [SerializeField] GameObject rectanglePrefab;
    [SerializeField] GameObject spawnCheckRectangle; //прямоугольник, проверяющий хватает ли места для добавления других прямоугольников
    [SerializeField] DrawLines drawLinesScript;

    //прямоугольник, который мы перетаскиваем в данный момент
    private GameObject draggedRectangle;

    private RaycastHit2D hit;

    //тэг ставим перетаскиваемому прямоугольнику, чтобы он не взаимодействоал с тестовым прямоугольником, которым проверяем коллизии
    public static string ignoreTriggerTag = "IgnoreTrigger";

    //проверка двойного клика
    private float clicked = 0;
    private float clicktime = 0;
    private float clickdelay = 0.5f;

    void Update()
    {
        SpawnOrDeleteRectangle();
        PickDraggedRectangle();
    }

    private void SpawnOrDeleteRectangle() //добавляем прямоугольник в пустое простанство, если тестовый прямоугольник под курсором ни с чем не сталкивается 
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        spawnCheckRectangle.transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);

        if (CheckIfCanPlaceRectangle.canPlaceRectangle)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(rectanglePrefab, new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0), Quaternion.identity);
            }
        }
        else if(hit)
        {
            if (DoubleClick())
            {
                IDestructable destructable = hit.collider.gameObject.GetComponent<IDestructable>();
                if (destructable != null)
                {
                    drawLinesScript.RemovePoint(hit.transform);
                    destructable.GetDestroyed();
                }
            }
        }
    }

    private void PickDraggedRectangle() //назначаем прямоугольник, который перетаскиваем по сцене, само перетаскивание в методе OnMauseDrag скрипта Rectangle
    {
        if (Input.GetMouseButton(0) && hit)
        {
            if (draggedRectangle == null)
            {
                draggedRectangle = hit.collider.gameObject;
                draggedRectangle.tag = ignoreTriggerTag;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (draggedRectangle != null)
            {
                draggedRectangle.tag = "Untagged";
                draggedRectangle = null;
            }
        }
    }
    private bool DoubleClick() //проверка на двойной клик мыши
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicked++;
            if (clicked == 1)
            {
                clicktime = Time.time;
            }
        }
        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            return true;
        }
        else if (clicked > 2 || Time.time - clicktime > 1)
        {
            clicked = 0;
        }
        
        return false;        
    }
}
