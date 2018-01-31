using DG.Tweening;
using UnityEngine;

public class Level002Indicator : MonoBehaviour
{
    public GameObject[] buttons;
    public Level002Board board;
    public SpriteRenderer connection;

    public void Check()
    {
        bool isIndicatorOff = true;
        for (int i = 0; i < buttons.Length; i++)
        {
            if (!buttons[i].GetComponent<Level002Cell>().isFaded)
            {
                isIndicatorOff = false;
                break;
            }
        }
        if (isIndicatorOff)
        {
            connection.color = Color.red;
            GetComponent<SpriteRenderer>().DOFade(0.5f, 0.3f);
            enabled = false;
        }
    }
}
