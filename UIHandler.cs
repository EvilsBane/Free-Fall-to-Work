using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    #region Objects, Variables
    public GameObject PauseUI;
    public GameObject GameUI;
    public GameObject FinishUI;
    public GameObject f_RestartButton;
    public GameObject f_NextButton;
    public Text levelTimeText;
    public Text finishText;

    private RectTransform rt;
    private RectTransform rtPauseUI;
    private RectTransform rtGameUI;
    private RectTransform rtFinishUI;
    private RectTransform f_rtRestartButton;

    private bool firstPlay;
    private int timeUpTextSize = 175;

    public int levelTimeValue = 25;

    private string timeUpString = "TIME!";
    private string finishWin = "Success!";
    private string finishLose = "Try Again";
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Pause();

        #region UI Setup
        rt = GetComponent<RectTransform>();
        rtPauseUI = PauseUI.GetComponent<RectTransform>();
        rtGameUI = GameUI.GetComponent<RectTransform>();
        rtFinishUI = FinishUI.GetComponent<RectTransform>();
        f_rtRestartButton = f_RestartButton.GetComponent<RectTransform>();
        rtPauseUI.sizeDelta = rt.sizeDelta;
        rtGameUI.sizeDelta = rt.sizeDelta;
        rtFinishUI.sizeDelta = rt.sizeDelta;
        #endregion

        firstPlay = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.endLevel == true)
        {
            Finish();
        }
    }

    #region UI Functions
    IEnumerator Countdown(int countdownTime, Text countdownText)
    {
        while(countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        countdownText.fontSize = timeUpTextSize;
        countdownText.text = timeUpString;
        GameManager.endLevel = true;
        GameManager.finishOnTime = false;
    }

    public void Resume()
    {
        if (firstPlay == true)
        {
            PauseUI.SetActive(false);
            GameUI.SetActive(true);
            Time.timeScale = 1f;
            GameManager.gameIsPaused = false;
            StartCoroutine(Countdown(levelTimeValue, levelTimeText));
            firstPlay = false;
        }
        else
        {
            PauseUI.SetActive(false);
            GameUI.SetActive(true);
            Time.timeScale = 1f;
            GameManager.gameIsPaused = false;
        }
    }

    public void Pause()
    {
        PauseUI.SetActive(true);
        GameUI.SetActive(false);
        Time.timeScale = 0f;
        GameManager.gameIsPaused = true;
    }

    public void RestartLevel()
    {
        FinishUI.SetActive(false);
        PauseUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.endLevel = false;
        GameManager.gameIsPaused = false;
    }

    public void Finish()
    {
        if (GameManager.finishOnTime == true)
        {
            PauseUI.SetActive(false);
            GameUI.SetActive(false);
            FinishUI.SetActive(true);
            Time.timeScale = 0f;
            GameManager.gameIsPaused = true;
            finishText.text = finishWin;
            
        }
        else
        {
            PauseUI.SetActive(false);
            GameUI.SetActive(false);
            FinishUI.SetActive(true);
            Time.timeScale = 0f;
            GameManager.gameIsPaused = true;
            finishText.text = finishLose;
            f_NextButton.SetActive(false);
            f_rtRestartButton.anchoredPosition = new Vector2(0f, f_rtRestartButton.anchoredPosition.y);
        }
        
    }
    #endregion

    #region Old Code
    /*
        private float currentTime = 0f;

    void Start()
    {
        //Output the current screen window height & width in the console
        Debug.Log("Screen Height: " + Screen.height + ", Screen Width: " + Screen.width);
        //Output the current camera height and width in the console
        Debug.Log("Camera Height: " + Camera.main.pixelHeight + ", Camera Width: " + Camera.main.pixelWidth);

        currentTime = levelTimeValue;
    }
        

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        levelTimeText.text = currentTime.ToString("0.0");
        if (currentTime <= 0)
        {
            currentTime = 0;
            levelTimeText.text = "0";
        }
    }
    */
    #endregion
}
