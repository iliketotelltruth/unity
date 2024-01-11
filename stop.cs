using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopgame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private bool close = false;
    private bool doublecheck = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            close = true;
            Debug.Log("Close Game?");
            if (Input.GetKeyDown("escape") && close)
            {
                doublecheck = true; 
            }
            else
            {
                doublecheck = false;
                close = false;
            }
        }
        if (doublecheck)
        {
            Application.Quit();
        }
    }
}
