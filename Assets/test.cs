using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision detected");
            if(collision.gameObject.tag.Equals("Enemy")) //sees if the object your player collided with has a tag called "Enemy", this can be replaced with if(coll.gameobject.name == ... but using a tag is an easy way to do it.
            {
                SceneManager.LoadScene("GameOver");
                Debug.Log("Working");
            }
        } 
}
