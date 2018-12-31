using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float PercentBoardScreen;
    private Vector2 sizeDisplay;

    private Vector2 dirCamera;
    // Start is called before the first frame update
    void Start()
    {
        sizeDisplay = new Vector2(Display.main.renderingWidth, Display.main.renderingHeight);
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        dirCamera = Vector2.zero;

        if ((mousePosition.x >= 0 && mousePosition.x <= sizeDisplay.x)
                    && (mousePosition.y >= 0 && mousePosition.y <= sizeDisplay.y))
        {
           
                if (mousePosition.y < sizeDisplay.y / 100 * PercentBoardScreen)
                {
                    dirCamera.y = -(1 - (mousePosition / (sizeDisplay / 100 * PercentBoardScreen)).y);
                }

          
                if (mousePosition.y > sizeDisplay.y / 100 * (100 - PercentBoardScreen))
                {
                    dirCamera.y = (mousePosition.y - sizeDisplay.y / 100 * (100 - PercentBoardScreen)) /(sizeDisplay.y / 100 * PercentBoardScreen);
                }

          
                if (mousePosition.x < sizeDisplay.x / 100 * 15)
                {
                    if (mousePosition.x < sizeDisplay.x / 100 * 5)
                    {
                        dirCamera.x = -1f;
                    }
                    else
                    {
                        dirCamera.x = -0.5f;
                    }
                }

                if (mousePosition.x > sizeDisplay.x / 100 * 85)
                {
                    if (mousePosition.x > sizeDisplay.x / 100 * 95)
                    {
                        dirCamera.x = 1;
                    }
                    else
                    {
                        dirCamera.x = 0.5f;
                    }
                }
            
        }
    }

    public bool KeyIsPress(KeyCode keyCode)
    {
        if (Input.GetKey(keyCode))
        {
            return true;
        }

        return false;
    }

    public Vector2 GetDirCamera()
    {
        return dirCamera;
    }

}
