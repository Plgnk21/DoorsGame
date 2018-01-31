using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public int levelsCount;
    public GameObject lastLevelText, nextLevelButton;

    [SerializeField]
    Transform holder;

    ResourceRequest request = null;
    GameObject current = null;
    int currentLevel;

    public static LevelManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start ()
    {
        Load(1);
    }


    public void Load(int level)
    {
        StartCoroutine(LoadLevel(level));
    }

    IEnumerator LoadLevel(int level)
    {
        if (current != null)
            UnLoad();
        currentLevel = level;
        request = Resources.LoadAsync(string.Format(@"Level{0:d3}", level));

        yield return request;
        
        current = Instantiate((GameObject)request.asset, holder);

        yield return null;
    }

    public void CompleteLevel()
    {
        nextLevelButton.SetActive(true);
    }

    public void LoadNextLevel()
    {
        if(++currentLevel < 4)
        {
            Load(currentLevel);
        }
        else
        {
            lastLevelText.SetActive(true);
        }
    }

    public void UnLoad()
    {
        if (current != null)
        {
            Destroy(current);
            current = null;
            Resources.UnloadUnusedAssets();
        }
    }
}