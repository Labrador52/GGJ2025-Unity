using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to keep the folder in the hierarchy
/// </summary>
public class FolderKeeper : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        // remove this script component
        Destroy(this);
    }
}
