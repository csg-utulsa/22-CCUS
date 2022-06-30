/**********
 * Created by: Coleton Wheeler
 * Created on: 3/1/22
 * 
 * Last edited by: N/A
 * Last edited on: N/A
 * 
 * Description: Disables all children of Managers gameobject from destrying on scene switch.
 *****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagersDontDestroy : MonoBehaviour
{

    private static ManagersDontDestroy instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("Managers exist. Deleting...");
            Destroy(this.gameObject);
        }
    }
}
