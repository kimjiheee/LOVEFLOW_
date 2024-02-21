using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public bool isHammerNearby = false;
    private bool isFallDown = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isFallDown)           
            transform.Translate(0, 0, 9.8f * 0.2f * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {      
        if (other.gameObject.CompareTag("Ground"))
        {
            isFallDown = false;
        }
    }
    
    void OnTriggerExit(Collider other)
    {        
        if (other.gameObject.CompareTag("Ground"))
        {
            isFallDown = true;
        }
    }
}
