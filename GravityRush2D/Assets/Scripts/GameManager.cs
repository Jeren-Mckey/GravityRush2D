using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    public delegate void OnPlayerHitDelegate();
    public static event OnPlayerHitDelegate playerHitDelegate;
    public static int CurrentLevel = 1;

    public static void OnPlayerHit()
    {
        playerHitDelegate();
    }
    
}
