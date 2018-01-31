using DG.Tweening; 
using UnityEngine;

public class Level003Flower : MonoBehaviour
{
    public GameObject pearl, bubble, angryFlower;
    public SpriteRenderer freshFlower;
    public SpriteRenderer crushedFlower;
    public SpriteRenderer openedDoor;

    void OnTriggerEnter2D(Collider2D collision)
    {
        pearl.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        freshFlower.DOFade(0.0f, 0.2f);
        crushedFlower.DOFade(1.0f, 0.2f);
        pearl.GetComponent<CircleCollider2D>().enabled = false;
        pearl.GetComponent<Transform>().DOMove(new Vector3(0.96f, 1.32f), 0.5f).OnComplete(() =>
        {
            openedDoor.DOFade(1.0f, 0.3f).SetDelay(0.4f).OnComplete(LevelManager.Instance.CompleteLevel);
            Deactivate();
            bubble.GetComponent<Level003Bubble>().Deactivate();
            angryFlower.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            angryFlower.GetComponent<Level003AngryFlower>().Deactivate();
        });
    }

    public void Deactivate()
    {
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
    }
}