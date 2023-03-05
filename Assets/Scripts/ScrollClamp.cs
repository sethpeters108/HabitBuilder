using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollClamp : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rectTransform.localPosition.y < 0)
        {
            rectTransform.localPosition = Vector3.zero;
        }else if (rectTransform.localPosition.y > 376)
        {
            rectTransform.localPosition = new Vector3(0,376,0);
        }
    }
}
