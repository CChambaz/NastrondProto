using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nastrond;

public class UIManager : MonoBehaviour
{
    [SerializeField] KeyCode hideUIKey;

    [Header("Game UI attributs")]
    [SerializeField] Canvas uiCanvas;
    [SerializeField] Text stoneQuantity;
    [SerializeField] Text basaltQuantity;
    [SerializeField] Text ironQuantity;
    [SerializeField] Text coalQuantity;
    [SerializeField] Text toolQuantity;
    [SerializeField] Text foodQuantity;
    [SerializeField] Text populationQuantity;

    Nastrond.GrowthSystem growthSystem;

    // Start is called before the first frame update
    void Start()
    {
        growthSystem = FindObjectOfType<Nastrond.GrowthSystem>();
    }

    // Update is called once per frame
    void Update()
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
