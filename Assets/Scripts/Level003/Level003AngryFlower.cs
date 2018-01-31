using UnityEngine;
using UnityEngine.EventSystems;

public class Level003AngryFlower : MonoBehaviour, IPointerClickHandler
{
    public SpriteRenderer pearl;
    public Level003Bubble bubble;
    public GameObject background;
    public Sprite pearlSprite;

    bool isActive;

    public bool IsPearlCatched { get; set; }

    bool isPossibleToGrab = false;

    const float force = 2.0f;

    Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!isActive)
        {
            isActive = true;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }

        if (!IsPearlCatched)
        {
            if (isPossibleToGrab)
            {
                pearl.transform.SetParent(transform, true);
                pearl.transform.localPosition = new Vector3(0.37f, -0.8f, -0.01f);
                IsPearlCatched = true;
                pearl.sprite = pearlSprite;
                bubble.ResetBuble();
            }            
        }
        else
        {
            IsPearlCatched = false;
            pearl.transform.SetParent(background.transform, true);
            pearl.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            pearl.GetComponent<CircleCollider2D>().enabled = true;
        }     
        
    }

    public void Deactivate()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        enabled = false;
    }

    void FixedUpdate()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.LeftArrow))
#else
        if (Input.acceleration.x <= -0.4f)
#endif
        {
            body.AddForce(Vector2.left * force);
        }
#if UNITY_EDITOR
        else if (Input.GetKey(KeyCode.RightArrow))
#else
        else if (Input.acceleration.x >= 0.4f)
#endif
        {
            body.AddForce(Vector2.right * force);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isPossibleToGrab = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isPossibleToGrab = false;
    }
}