using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button buttonStartGame;
    [SerializeField] private Button buttonAbout;
    [SerializeField] private Button buttonQuit;
    
    private void Awake()
    {
        // buttonStartGame.onClick.AddListener(StartGame);
        // buttonAbout.onClick.AddListener(About);
        buttonQuit.onClick.AddListener(GameManager.Quit);

        if (GameManager.GameState == GameState.StartMenu)
        {
            gameObject.SetActive(true);

        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
