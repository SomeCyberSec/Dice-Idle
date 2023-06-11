using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Button upgradeButton;
    public UpgradeHandler selectedUpgradeHandler;

    void Start()
    {
        upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
    }

    void OnUpgradeButtonClick()
    {
        // Call the upgrade function on the selected instance
        if (selectedUpgradeHandler != null)
        {
            selectedUpgradeHandler.UpgradeRollMultiplier();
        }
    }

    public void SetSelectedUpgradeHandler(UpgradeHandler upgradeHandler)
    {
        selectedUpgradeHandler = upgradeHandler;
    }
}