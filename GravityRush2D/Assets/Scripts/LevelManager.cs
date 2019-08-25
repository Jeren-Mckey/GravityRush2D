using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject playerObject;
    public delegate void OnPlayerHitDelegate();
    public static event OnPlayerHitDelegate playerHitDelegate;

    public static void OnPlayerHit()
    {
        playerHitDelegate();
    }
    
}
