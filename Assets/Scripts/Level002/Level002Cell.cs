using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Level002Cell : MonoBehaviour, IPointerClickHandler
{
    public Level002Board board;
    public int x, y, n;
    public string color;
    public SpriteRenderer figure;
    public Transform glow;
    public GameObject[] workingConnections;
    public bool isFaded = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        Level002Cell cb = board.currentButton;

        if (isFaded)
        {
            board.ResetBoard();
        }
        else
        {
            if (board.firstButton == this)
            {
                glow.GetComponent<SpriteRenderer>().DOFade(0.0f, 0.1f);
                isFaded = true;
            }
            else
            {
                bool detected = false;
                for (int i = 0; i < cb.workingConnections.Length && !detected; i++)
                {
                    for (int j = 0; j < workingConnections.Length; j++)
                    {
                        if (cb.workingConnections[i] == workingConnections[j] && cb.isFaded)
                        {
                            cb.workingConnections[i].GetComponent<SpriteRenderer>().color = Color.red;
                            cb.workingConnections[i].GetComponent<Transform>().DOMoveZ(-0.2f, 0.1f);
                            isFaded = true;
                            glow.GetComponent<SpriteRenderer>().DOFade(0.0f, 0.1f);
                            board.currentButton = this;
                            detected = true;
                            break;
                        }
                    }
                }
                if (!detected)
                {
                    board.ResetBoard();
                }
            }
        }        
        for (int i = 0; i < board.boardIndicators.Length; i++)
        {
            board.boardIndicators[i].GetComponent<Level002Indicator>().Check();
        }
        board.CheckVictory();
    }

    public void Deactivate()
    {
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
    }

    void Reset()
    {
        x = int.Parse(transform.name.Substring(0, 1));
        y = int.Parse(transform.name.Substring(2, 1));
        n = int.Parse(transform.name.Substring(6, 1));
        color = (transform.name.Substring(4, 1));
        transform.position = new Vector3(-1.5f + y, 1.5f - x, -0.2f);
        board = transform.parent.GetComponent<Level002Board>();
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = board.figures[n - 1];
        glow = transform.GetChild(1);

        switch (color)
        {
            case "B":
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.cyan;
                transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.cyan;
                break;
            case "G":
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case "Y":
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
                transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case "P":
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.magenta;
                transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.magenta;
                break;
        }
    }
}
