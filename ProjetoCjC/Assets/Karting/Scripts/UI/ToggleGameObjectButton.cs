using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleGameObjectButton : MonoBehaviour
{
    public GameObject objectToToggle;
    public GameObject mainScenekart1;
    public GameObject mainScenekart2;
    public bool resetSelectionAfterClick;
    public GameObject kartPlayer;

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
        Debug.Log(objectToToggle.tag);
        if (!objectToToggle.CompareTag("CHMenu"))
        {
            if (mainScenekart1 != null)
                mainScenekart1.SetActive(!active);
            if (kartPlayer != null)
                kartPlayer.SetActive(!active);
        }
        if (mainScenekart2 != null)
            mainScenekart2.SetActive(!active);

        if (resetSelectionAfterClick)
            EventSystem.current.SetSelectedGameObject(null);
    }
}
