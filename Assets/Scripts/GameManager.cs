using UnityEngine;
using System.Collections.Generic; // lists...
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<string> levels;

    public int hp = 3;
    public int currentLevel = 0;

    private bool isTeleporting = false;
    private bool isDead = false;

    // singleton
    public static GameManager instance = null;
    private void Start()
    {
        if(instance == null) // there's no second gamemanager
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else // if there's a second gamemanager - destroy it
        {
            Destroy(this);
        }
    }

    public void Win()
    {
        if(!isTeleporting)
        {
            currentLevel++;
            isTeleporting = true;
            Invoke("LoadNextLevel", 1f);
            isTeleporting = false;
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(levels[currentLevel]);
    }

    public void Lose()
    {
        if (!isDead)
        {
            isDead = true;
            hp--;

            if (hp <= 0)
            {
                currentLevel = 0;
                hp = 3;
            }
            Invoke("LoadNextLevel", 2f);
            isDead = false;
        }
    }
}
