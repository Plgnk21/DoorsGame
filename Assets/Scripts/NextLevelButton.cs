using UnityEngine;
using UnityEngine.EventSystems;

public class NextLevelButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        LevelManager.Instance.LoadNextLevel();
        gameObject.SetActive(false);
    }
}
