using DG.Tweening;
using UnityEngine;

public class Level002Board : MonoBehaviour
{
    public Sprite[] figures;
    public GameObject[] buttons;
    public GameObject[] boardIndicators;
    public Level002Cell currentButton;
    public Level002Cell firstButton;
    public SpriteRenderer light;
    public SpriteRenderer glowingBox;
    public SpriteRenderer indicators;
    public SpriteRenderer door;


    public void ResetBoard()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Level002Cell>().glow.GetComponent<SpriteRenderer>().DOFade(1.0f, 0.1f);
            buttons[i].GetComponent<Level002Cell>().isFaded = false;
            for (int j = 0; j < buttons[i].GetComponent<Level002Cell>().workingConnections.Length; j++)
            {
                buttons[i].GetComponent<Level002Cell>().workingConnections[j].GetComponent<SpriteRenderer>().color = Color.cyan;
            }
        }
        currentButton = firstButton;
    }

    public void CheckVictory()
    {
        bool isVictory = true;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (!buttons[i].GetComponent<Level002Cell>().isFaded)
            {
                isVictory = false;
                break;
            }
        }
        if (isVictory)
        {
            enabled = false;
            gameObject.SetActive(false);
            light.DOFade(0.0f, 0.3f);
            indicators.DOFade(0.5f, 0.3f);
            glowingBox.DOFade(0.0f, 0.3f).OnComplete(()=> door.DOFade(0.0f, 0.3f)).OnComplete(LevelManager.Instance.CompleteLevel);
            foreach (GameObject button in buttons)
            {
                button.GetComponent<Level002Cell>().Deactivate();
            }
            enabled = false;
        }
    }
}
