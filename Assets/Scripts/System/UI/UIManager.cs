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
    Nastrond.ResourceCounterSystem ressourcesCounter;

    GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas.gameObject.SetActive(false);

        gameState = GameState.PLAY;

        growthSystem = FindObjectOfType<Nastrond.GrowthSystem>();
        ressourcesCounter = FindObjectOfType<Nastrond.ResourceCounterSystem>();
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

            if (ressourcesCounter != null)
            {
                if (stoneQuantity.isActiveAndEnabled)
                    stoneQuantity.text = ressourcesCounter.GetStoneAmount().ToString();

                if (basaltQuantity.isActiveAndEnabled)
                    basaltQuantity.text = ressourcesCounter.GetBasaltAmount().ToString();

                if (ironQuantity.isActiveAndEnabled)
                    ironQuantity.text = ressourcesCounter.GetIronAmount().ToString();

                if (coalQuantity.isActiveAndEnabled)
                    coalQuantity.text = ressourcesCounter.GetCoalAmount().ToString();      

                if (toolQuantity.isActiveAndEnabled)
                    toolQuantity.text = ressourcesCounter.GetToolAmount().ToString();      

                if (foodQuantity.isActiveAndEnabled)
                    foodQuantity.text = ressourcesCounter.GetFoodAmount().ToString();      
            }
            else
            {
                // Show example value
                if (stoneQuantity.isActiveAndEnabled)
                    stoneQuantity.text = "18623";

                if (basaltQuantity.isActiveAndEnabled)
                    basaltQuantity.text = "50301";

                if (ironQuantity.isActiveAndEnabled)
                    ironQuantity.text = "69696";

                if (coalQuantity.isActiveAndEnabled)
                    coalQuantity.text = "12345";

                if (toolQuantity.isActiveAndEnabled)
                    toolQuantity.text = "98765";

                if (foodQuantity.isActiveAndEnabled)
                    foodQuantity.text = "25896";
            }

            if (growthSystem != null && populationQuantity.isActiveAndEnabled)
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
