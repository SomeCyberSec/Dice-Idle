using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public static DiceManager instance;
    public List<GameObject> dicePrefabs;
    
    private List<GameObject> diceInstances = new List<GameObject>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("More than one DiceManager in the scene!");
        }
    }

    void Start()
    {
        // Assuming that the order of the prefabs in the list matches the order of the DiceType enum
        for (int i = 0; i < dicePrefabs.Count; i++)
        {
            UpgradeHandler upgradeHandler = dicePrefabs[i].GetComponent<UpgradeHandler>();
            if (upgradeHandler != null)
            {
                upgradeHandler.diceType = (DiceType)i;
            }
        }
    }

    public void CreateDice(int selection)
    {
        GameObject currentContainer = DiceSelectMenu.instance.CurrentContainer;
        if (currentContainer == null)
        {
            Debug.LogError("No container selected!");
            return;
        }
        Vector3 position = currentContainer.transform.localPosition;
        position += new Vector3(395f, 643f, 0);
        
        // Instantiate the selected dice and grab necessary components
        GameObject selectedDice = Instantiate(dicePrefabs[selection], position, Quaternion.identity, transform);
        UpgradeHandler upgradeHandler = selectedDice.GetComponent<UpgradeHandler>();

        // Add upgrades to the dice (decrease by 25% if 0.25f)
        upgradeHandler.UpgradeTimeBetweenRolls(0.5f);

        // Change name for easier access
        selectedDice.name = "Dice" + selection;

        // Add dice to list for access to dice instances later
        diceInstances.Add(selectedDice);
        
        // Destroy the menu
        DiceSelectMenu.instance.DestroyDiceSelectMenu();

        // Destroy the container this is being placed in
        Destroy(currentContainer);
    }
}
