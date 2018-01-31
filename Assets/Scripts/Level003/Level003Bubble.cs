using UnityEngine;
using DG.Tweening;

public class Level003Bubble : MonoBehaviour
{
    public SpriteRenderer pearl;
    public Transform background;
    public Level003AngryFlower hunter;
    public Sprite pearlSprite;

    Vector3 startPos;

    void RaisePearl()
    {
        transform.DOLocalMoveY(2.25f, 3.0f).SetEase(Ease.InOutFlash).OnComplete(()=>
        {
            transform.DOLocalMoveY(1.0f, 1.5f).SetEase(Ease.InOutFlash).OnComplete(() =>
            {
                transform.DOLocalMoveY(2.5f, 2.0f).SetEase(Ease.InOutFlash).OnComplete(() =>
                {
                    if (!hunter.IsPearlCatched)
                    {
                        pearl.transform.SetParent(background, true);
                        pearl.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    }

                    GetComponent<SpriteRenderer>().DOFade(0.0f, 0.3f);
                    transform.DOLocalMoveY(-1.25f, 0.0f);
                });
            });
        });
    }

    public void ResetBuble()
    {
        transform.DOKill();
        transform.position = startPos;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        pearl.sprite = pearlSprite;
        pearl.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        pearl.transform.SetParent(transform, true);
        pearl.transform.localPosition = new Vector3(0.0f, 0.0f, -0.01f);
        pearl.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<SpriteRenderer>().DOFade(1.0f, 0.3f);
        RaisePearl();
    }

    void Start ()
    {
        startPos = transform.position;
        RaisePearl();
	}

    public void Deactivate()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D c in colliders)
        {
            c.enabled = false;
        }
        enabled = false;
    }
}