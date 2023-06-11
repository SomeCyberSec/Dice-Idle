using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class DiceSelectMenu : MonoBehaviour
{
    public static DiceSelectMenu instance;
    public GameObject menuPrefab;
    public List<SpriteAtlas> atlases;
    public GameObject squareButton;
    public GameObject CurrentContainer { get; set; }

    private GameObject currentMenu; // This will store the menu instance

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("More than one DiceSelectMenu in the scene!");
        }
    }

    public void CreateDiceSelectMenu()
    {
        // Check if a menu already exists
        if(currentMenu == null)
        {
            // Only create a new menu if one doesn't already exist
            currentMenu = Instantiate(menuPrefab, new Vector3(320, 151, 0), Quaternion.identity, transform);

            // Fill menu with dice selections
            PopulateDiceSelectMenu();

            // Find the 'Close' child button and add a listener to it
            Button closeButton = currentMenu.transform.Find("Close").GetComponent<Button>();
            closeButton.onClick.AddListener(DestroyDiceSelectMenu);
        }
        else
        {
            Debug.LogWarning("Attempted to create a new menu when one already exists!");
        }
    }

    public void DestroyDiceSelectMenu()
    {
        // Check if a menu exists
        if(currentMenu != null)
        {
            // Destroy the menu and nullify the reference
            Destroy(currentMenu);
            currentMenu = null;
        }
        else
        {
            Debug.LogWarning("Attempted to destroy the menu when none exists!");
        }
    }

    public void PopulateDiceSelectMenu()
    {
        Transform viewport = currentMenu.transform.Find("Viewport");
        Transform content = viewport.transform.Find("Content");

        bool position1 = false;
        bool position2 = false;
        bool position3 = false;
        int rowCount = 0;
        for(int i = 0; i < atlases.Count; ++i)
        {
            GameObject diceButton = Instantiate(squareButton, content.transform);
            
            // Layout for the dice in the selection menu
            if(position1 == false)
            {
                diceButton.GetComponent<RectTransform>().localPosition = new Vector3(-185, -150-(185*rowCount), 0);
                position1 = true;
            }
            else if(position2 == false)
            {
                diceButton.GetComponent<RectTransform>().localPosition = new Vector3(0, -150-(185*rowCount), 0);
                position2 = true;
            }
            else if(position3 == false)
            {
                diceButton.GetComponent<RectTransform>().localPosition = new Vector3(185, -150-(185*rowCount), 0);
                position3 = true;
            }
            else
            {
                ++rowCount;
                diceButton.GetComponent<RectTransform>().localPosition = new Vector3(-185, -150-(185*rowCount), 0);
                position2 = false;
                position3 = false;
            }
            
            // Select the sprite with the dice's fifth side
            Sprite sprite = atlases[i].GetSprite(atlases[i].name + "5");
            Image imageComponent = diceButton.GetComponent<Image>();
            imageComponent.sprite = sprite;

            // Add CreateDice function to the dice in the menu
            Button buttonComponent = diceButton.GetComponent<Button>();
            int index = i;
            buttonComponent.onClick.AddListener(() => { DiceManager.instance.CreateDice(index); });
        }
    }
}