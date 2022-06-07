using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy_Behavior : MonoBehaviour
{
    public GameBehavior gameManagerChange;
   void Start()
   {
      gameManagerChange = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
    
   }
    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        gameManagerChange.Energy = 1000;
       Destroy(this.transform.parent.gameObject);
    }
    
    
}
