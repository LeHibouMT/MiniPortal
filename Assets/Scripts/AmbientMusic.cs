using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientMusic : MonoBehaviour
{
    public static AmbientMusic AmbientInstance;

    private void awake()
    {
        if(AmbientInstance != null && AmbientInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        AmbientInstance = this;
        DontDestroyOnLoad(this);
    }
}
