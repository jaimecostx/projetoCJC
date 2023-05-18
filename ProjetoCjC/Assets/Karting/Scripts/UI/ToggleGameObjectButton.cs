using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleGameObjectButton : MonoBehaviour
{
    public GameObject objectToToggle;
    public GameObject mainScenekart1;
    public GameObject mainScenekart2;
    public bool resetSelectionAfterClick;

    void Update()
    {
        if (objectToToggle.activeSelf && Input.GetButtonDown(GameConstants.k_ButtonNameCancel))
        {
            SetGameObjectActive(false);
        }
    }

    public void SetGameObjectActive(bool active)
    {
        objectToToggle.SetActive(active);

        if (mainScenekart1 != null)
            mainScenekart1.SetActive(!active);
        if (mainScenekart2 != null)
            mainScenekart2.SetActive(!active);

        if (resetSelectionAfterClick)
            EventSystem.current.SetSelectedGameObject(null);
    }
}
