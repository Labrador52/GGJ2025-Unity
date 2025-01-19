using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
#region
    private static StartMenu _instance;
    public static StartMenu Instance
    {
        get
        {
            return _instance;
        }
    }
#endregion


    [SerializeField] private Button buttonStartGame;
    [SerializeField] private Button buttonAbout;
    [SerializeField] private Button buttonQuit;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        buttonStartGame.onClick.AddListener(Gameplay.Instance.StartGame);
        buttonQuit.onClick.AddListener(GameManager.Quit);

        buttonStartGame.onClick.AddListener(PlayButtonSFX);
        buttonAbout.onClick.AddListener(PlayButtonSFX);
        buttonQuit.onClick.AddListener(PlayButtonSFX);


        if (GameManager.GameState == GameState.StartMenu)
        {
            gameObject.SetActive(true);

        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void PlayButtonSFX()
    {
        AudioManager.instance.PlaySFX(2);
    }
}
