using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sfx : MonoBehaviour
{
    AudioSource audio_;

    // Start is called before the first frame update
    void Start()
    {
        audio_ = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Word3D"))
        {
            if(audio_ != null)
            {
                audio_.Play();
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
