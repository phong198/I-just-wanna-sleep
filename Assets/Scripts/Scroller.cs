using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    //public float scrollSpeed;
    public RawImage background;
    public float _x, _y;

    // Update is called once per frame
    void Update()
    {
        background.uvRect = new Rect(background.uvRect.position + new Vector2(_x,_y) * Time.deltaTime * 1f, background.uvRect.size);
    }
}
