using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle : MonoBehaviour, IDestructable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDestroyed()
    {
        Destroy(gameObject);
    }

    private void OnMouseDrag()
    {
        if(CheckIfCanPlaceRectangle.canPlaceRectangle)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);
        }
    }
}
