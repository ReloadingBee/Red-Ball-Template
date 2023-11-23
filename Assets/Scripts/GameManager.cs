using UnityEngine;
using System.Collections.Generic; // lists...
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<string> levels;

    public int hp = 3;
    public int currentLevel = 0;
    public Transform transition;

    private bool isTeleporting = false;
    private bool isDead = false;
    private Vector3 transitionTargetScale;

    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip gameOverSound;
    AudioSource source;

    // singleton
    public static GameManager instance = null;
    private void Start()
    {
        source = GetComponent<AudioSource>();
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

    private void Update()
    {
        transition.localScale = Vector3.MoveTowards(transition.localScale, transitionTargetScale, 50 * Time.deltaTime);
    }

    public void Win()
    {
        if(!isTeleporting)
        {
            currentLevel++;
            isTeleporting = true;
            Invoke("LoadNextLevel", 1f);
            transitionTargetScale = Vector3.one * 25;
            //source.clip = winSound;
            //source.Play();
            source.PlayOneShot(winSound);
        }
    }

    void LoadNextLevel()
    {
        isDead = false;
        isTeleporting = false;
        SceneManager.LoadScene(levels[currentLevel]);
        transitionTargetScale = Vector3.zero;
    }

    public void Lose()
    {
        if (!isDead && !isTeleporting)
        {
            isDead = true;
            hp--;
            if(hp > 0)
            {
                source.PlayOneShot(loseSound);
            }
            if (hp <= 0)
            {
                currentLevel = 0;
                hp = 3;
                source.PlayOneShot(gameOverSound);
            }
            Invoke("LoadNextLevel", 2f);
            transitionTargetScale = Vector3.one * 25;
        }
    }
}
