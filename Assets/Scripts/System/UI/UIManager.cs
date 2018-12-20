using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nastrond;

public class UIManager : MonoBehaviour
{
    enum GameState
    {
        PLAY,
        PAUSE
    }

    [SerializeField] KeyCode hideUIKey;

    [Header("Game UI attributs")]
    [SerializeField] Canvas uiCanvas;
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] Text stoneQuantity;
    [SerializeField] Text basaltQuantity;
    [SerializeField] Text ironQuantity;
    [SerializeField] Text coalQuantity;
    [SerializeField] Text toolQuantity;
    [SerializeField] Text foodQuantity;
    [SerializeField] Text populationQuantity;

    Nastrond.GrowthSystem growthSystem;

    GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas.gameObject.SetActive(false);

        gameState = GameState.PLAY;

        growthSystem = FindObjectOfType<Nastrond.GrowthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            SwitchUI();

        if(gameState == GameState.PLAY)
        { 
            // Hide or show the UI if corresponding key is pressed
            if (Input.GetKeyUp(hideUIKey))
                uiCanvas.gameObject.SetActive(!uiCanvas.isActiveAndEnabled);

            if (stoneQuantity.isActiveAndEnabled)
                stoneQuantity.text = "18623";       // Example value

            if (basaltQuantity.isActiveAndEnabled)
                basaltQuantity.text = "50301";       // Example value

            if (ironQuantity.isActiveAndEnabled)
                ironQuantity.text = "69696";          // Example value

            if (coalQuantity.isActiveAndEnabled)
                coalQuantity.text = "12345";        // Example value

            if (toolQuantity.isActiveAndEnabled)
                toolQuantity.text = "98765";        // Example value

            if (foodQuantity.isActiveAndEnabled)
                foodQuantity.text = "25896";        // Example value

            if (populationQuantity.isActiveAndEnabled)
                populationQuantity.text = growthSystem.GetPopulationCount() + "/" + growthSystem.GetPopulationCapacity();
        }
    }

    void SwitchUI()
    {
        switch(gameState)
        {
            case GameState.PLAY:
                uiCanvas.gameObject.SetActive(false);
                pauseCanvas.gameObject.SetActive(true);
                Time.timeScale = 0;
                gameState = GameState.PAUSE;
                break;
            case GameState.PAUSE:
                Resume();
                break;
        }
    }

    public void Resume()
    {
        uiCanvas.gameObject.SetActive(true);
        pauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
        gameState = GameState.PLAY;
    }
}
