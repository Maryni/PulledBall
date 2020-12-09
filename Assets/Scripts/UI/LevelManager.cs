using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void Exit(){ Application.Quit(); }
    public void LoadLevel() { SceneManager.LoadSceneAsync("Game"); }
    
}
