using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class DiceHandler : MonoBehaviour
{
    public SpriteAtlas diceAtlas;
    public UpgradeHandler upgradeHandler;

    private CurrencyHandler score;
    private Sprite newDiceSprite;
    private int fracture = 1;

    void Start()
    {
        score = CurrencyHandler.instance;
        newDiceSprite = diceAtlas.GetSprite(gameObject.tag + "5");
        GetComponent<Image>().sprite = newDiceSprite;

        if(upgradeHandler == null)
        {
            upgradeHandler = GetComponent<UpgradeHandler>();
        }
    }

    public void OnClick()
    {
        diceRng();
    }

    private void diceRng()
    {
        int diceRoll = Random.Range(1, 7);
        
        // Uses multiplier from UpgradeHandler
        score.scoreIncrease(diceRoll, upgradeHandler.multiplier);
        
        newDiceSprite = diceAtlas.GetSprite(gameObject.tag + diceRoll * fracture);
        GetComponent<Image>().sprite = newDiceSprite;
    }
}
