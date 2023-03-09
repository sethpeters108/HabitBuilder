using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToPage(string pageName)
    {
        
        for(int i = 0; i < pages.Length; i++)
        {
            Debug.Log(pages[i].name);
            if (!pages[i].name.Equals(pageName))
            {
                pages[i].SetActive(false);
            }
            else
            {
                pages[i].SetActive(true);
            }
        }
    }

}
