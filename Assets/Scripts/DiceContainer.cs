using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceContainer : MonoBehaviour
{
    public GameObject containerPrefab;
    public float spacing = 1f;

    private DiceSelectMenu diceSelectMenu;
    private List<GameObject> containerList = new List<GameObject>();
    private List<PositionStatus> positions = new List<PositionStatus>
    {
        new PositionStatus(new Vector2(0, 0), false),
        new PositionStatus(new Vector2(0, 150), false),
        new PositionStatus(new Vector2(150, 0), false),
        new PositionStatus(new Vector2(0, -150), false),
        new PositionStatus(new Vector2(-150, 0), false),
        new PositionStatus(new Vector2(150, 150), false),
        new PositionStatus(new Vector2(150, -150), false),
        new PositionStatus(new Vector2(-150, -150), false),
        new PositionStatus(new Vector2(-150, 150), false),
    };
    private PositionStatus firstAvailable = null;
    private GameObject newContainer;

    void Start()
    {
        diceSelectMenu = DiceSelectMenu.instance;
    }

    public void CreateContainer()
    {
        foreach(PositionStatus posStatus in positions)
        {
            if(!posStatus.IsOccupied)
            {
                firstAvailable = posStatus;
                posStatus.IsOccupied = true;
                break;
            }
        }

        if(firstAvailable != null)
        {
            newContainer = Instantiate(containerPrefab, Vector3.zero, Quaternion.identity, transform);
            (newContainer.transform as RectTransform).anchoredPosition = firstAvailable.Position;
            containerList.Add(newContainer);
        }

        // Get the Button component and add a click listener
        Button button = newContainer.GetComponent<Button>();
        GameObject containerForListener = newContainer;
        button.onClick.AddListener(() => SelectContainer(containerForListener));
    }

    public void SelectContainer(GameObject selectedContainer)
    {
        diceSelectMenu.CurrentContainer = selectedContainer;
        diceSelectMenu.CreateDiceSelectMenu();
    }
}
