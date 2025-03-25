using UnityEngine;
using UnityEngine.InputSystem;

public class SettingsButtonHandler : MonoBehaviour
{
    public InputActionReference settingsButton; // Assign in the Inspector

    public GameObject SettingsMenu;

    private void OnEnable()
    {
        settingsButton.action.Enable();
        settingsButton.action.performed += OpenSettingsMenu;
    }

    private void OnDisable()
    {
        settingsButton.action.performed -= OpenSettingsMenu;
        settingsButton.action.Disable();
    }

    private void OpenSettingsMenu(InputAction.CallbackContext context)
    {

        if (SettingsMenu.activeInHierarchy)
        {
            SettingsMenu.SetActive(false);
        }
        else
        {
            SettingsMenu.SetActive(true);
        }
        Debug.Log("Settings (Menu) button on left controller pressed! Open settings menu here.");
        // Implement your settings menu logic
    }
}
