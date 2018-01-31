using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;

public class Level001Key : MonoBehaviour, IPointerClickHandler
{
    public SpriteRenderer door;

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.DOMove(new Vector3(0.0f, 0.0f, -0.1f), 0.5f).OnComplete(()=>
        {
            GetComponent<SpriteRenderer>().DOFade(0.0f, 0.3f);
            door.DOFade(1.0f, 0.3f).OnComplete(LevelManager.Instance.CompleteLevel);
            Deactivate();
        });
    }

    public void Deactivate()
    {
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
    }
}