using UnityEngine;

//висит на прямоугольнике, котоырый движется под курсором мыши
public class CheckIfCanPlaceRectangle : MonoBehaviour
{
    public static bool canPlaceRectangle = true;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!collision.CompareTag(RectangleManager.ignoreTriggerTag))
            canPlaceRectangle = false;
        else
            canPlaceRectangle = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(RectangleManager.ignoreTriggerTag))
            canPlaceRectangle = true;
    }
}
