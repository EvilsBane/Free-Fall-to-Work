using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public static bool gameIsPaused;
    public static bool endLevel;
    public static bool finishOnTime;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        endLevel = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsPaused)
        {
            Debug.Log("Game Is Paused");
        }

        if (!gameIsPaused)
        {
            Debug.Log("Game Is Resumed");
        }
    }
}
