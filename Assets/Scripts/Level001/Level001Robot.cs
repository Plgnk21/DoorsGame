using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Level001Robot : MonoBehaviour, IPointerClickHandler
{
    public SpriteRenderer healthyRobot;
    public SpriteRenderer damagedRobot;
    public GameObject key;
    public ParticleSystem sparks, smoke;

    byte health = 4;
    Vector2 touchPos;
    Animator anim;
    Camera main;

    const float center = 0.4f;

    void Awake()
    {
        anim = GetComponent<Animator>();
        main = Camera.main;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        touchPos = main.ScreenToWorldPoint(eventData.position);
        if (touchPos.x - transform.position.x > center)
        {
            sparks.Play();
            anim.SetBool("IsRightImpact", true);
        }
        else if (touchPos.x - transform.position.x < -center)
        {
            sparks.Play();
            anim.SetBool("IsLeftImpact", true);
        }
        else
        {
            smoke.Play();
            StartCoroutine(SmokeBlock());
        }           
    }

    IEnumerator SmokeBlock()
    {
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        GetComponent<Collider2D>().enabled = true;
        yield return null;
    }

    public void EndImpact()
    {
        health--;
        anim.SetBool("IsLeftImpact", false);
        anim.SetBool("IsRightImpact", false);
        ShowDamage();
    }

    void ShowDamage()
    {
        if (health != 4)
        {
            healthyRobot.DOFade(0.0f, 0.3f);
        }
        if (health == 0)
        {
            GetComponent<Collider2D>().enabled = false;
            damagedRobot.DOFade(0.0f, 0.3f);
            anim.SetBool("IsDead", true);
            anim.enabled = false;
            transform.DOMove(new Vector3(transform.position.x, -2.0f, transform.position.z), 0.5f);
            key.GetComponent<Collider2D>().enabled = true;
            Deactivate();
        }
    }

    public void Deactivate()
    {
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
    }
}