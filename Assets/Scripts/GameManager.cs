using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gmInstance;

    [SerializeField] MusicManager musicPrefab;

    MusicManager musicManager;

    private void Awake()
    {
        if (gmInstance == null)
            gmInstance = this;
        else if (gmInstance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();

        if (musicManager == null)
            musicManager = Instantiate(musicPrefab);

        DontDestroyOnLoad(musicManager);
    }

    public void LoadScene(int scene_id)
    {
        SceneManager.LoadScene(scene_id);
    }

    public int GetSceneID()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
