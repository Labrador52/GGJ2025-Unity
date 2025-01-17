using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScreen : MonoBehaviour
{
#region Singleton
    private TransitionScreen _instance;
    public TransitionScreen Instance
    {
        get
        {
            return _instance;
        }
    }
#endregion

    private void Awake()
    {
#region Singleton
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
#endregion

        gameObject.SetActive(false);    // hide the transition screen
    }
}
