using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Level002Box : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
    public SpriteRenderer leftArrow;
    public SpriteRenderer downArrow;
    public GameObject gameBoard;

    Vector2 touchPos;
    Camera main;
    byte lockStage = 2;

    public void OnPointerDown(PointerEventData eventData)
    {
        touchPos = main.ScreenToWorldPoint(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector2 endDragPos = main.ScreenToWorldPoint(eventData.position);
        if (touchPos.x - endDragPos.x > 0.5f && lockStage == 2)
        {
            lockStage--;
            leftArrow.DOFade(0.0f, 0.3f).OnComplete(() =>
            {
                downArrow.DOFade(1.0f, 0.3f);
            });

        }
        if (touchPos.y - endDragPos.y > 0.5f && lockStage == 1)
        {
            downArrow.DOFade(0.0f, 0.3f).OnComplete(() =>
            {
                gameBoard.SetActive(true);
                Deactivate();
            });
        }
    }

    public void Deactivate()
    {
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
    }

    void Awake()
    {
        main = Camera.main;
    }
}
