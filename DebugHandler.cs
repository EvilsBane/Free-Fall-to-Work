using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugHandler : MonoBehaviour
{
    #region Objects, Variables
    public TouchController touchController;
    public UIHandler uiHandler;
    public Camera cam;
    public Toggle camToggle;
    public RectTransform rtPauseButton;
    public GameObject[] nets;
    public GameObject[] acs;

    public InputField timerInput;
    public InputField xMoveInput;
    public InputField yMoveInput;
    public InputField slowMoveInput;
    public InputField slowTimeInput;
    public Slider fovSlider;
    public Slider xPosSlider;
    public Slider zPosSlider;
    public Slider netSilder;
    public Slider acSlider;

    private float camRotZFlip = 180f;
    private float camRotZ = 0f;
    private float camRotX = 100f;
    private float posYPauseButton = 125f;
    private float posYLevelTimeText = 150f;
    #endregion

    public void CameraFlip()
    {
        if (camToggle.isOn)
        {
            cam.transform.rotation = Quaternion.Euler(cam.transform.eulerAngles.x, camRotZFlip, 0f);

            rtPauseButton.anchorMax = Vector2.zero;
            rtPauseButton.anchorMin = Vector2.zero;
            rtPauseButton.anchoredPosition = new Vector2(rtPauseButton.anchoredPosition.x, posYPauseButton);

            uiHandler.levelTimeText.rectTransform.anchorMax = new Vector2(0.5f, 0f);
            uiHandler.levelTimeText.rectTransform.anchorMin = new Vector2(0.5f, 0f);
            uiHandler.levelTimeText.rectTransform.anchoredPosition = new Vector2(uiHandler.levelTimeText.rectTransform.anchoredPosition.x, posYLevelTimeText);
        }
        else
        {
            cam.transform.rotation = Quaternion.Euler(camRotX, camRotZ, 0f);

            rtPauseButton.anchorMax = Vector2.up;
            rtPauseButton.anchorMin = Vector2.up;
            rtPauseButton.anchoredPosition = new Vector2(rtPauseButton.anchoredPosition.x, -posYPauseButton);

            uiHandler.levelTimeText.rectTransform.anchorMax = new Vector2(0.5f, 1f);
            uiHandler.levelTimeText.rectTransform.anchorMin = new Vector2(0.5f, 1f);
            uiHandler.levelTimeText.rectTransform.anchoredPosition = new Vector2(uiHandler.levelTimeText.rectTransform.anchoredPosition.x, -posYLevelTimeText);

        }
    }

    #region Change Functions
    public void ChangeTimer()
    {
        uiHandler.levelTimeValue = int.Parse(timerInput.text);
    }

    public void ChangeHorizontalMove()
    {
        touchController.moveSpeed = float.Parse(xMoveInput.text);
    }

    public void ChangeFallSpeed()
    {
        touchController.fallSpeed = float.Parse(yMoveInput.text);
    }

    public void ChangeSlowFallSpeed()
    {
        touchController.slowFall = float.Parse(slowMoveInput.text);
    }

    public void ChangeSlowFallTime()
    {
        touchController.slowTime = float.Parse(slowTimeInput.text);
    }

    public void ChangeFOV()
    {
        cam.fieldOfView = fovSlider.value;
    }

    public void ChangeCameraXPosition()
    {
        cam.transform.position = new Vector3(xPosSlider.value, cam.transform.position.y, cam.transform.position.z);
    }

    public void ChangeCameraZPosition()
    {
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, zPosSlider.value);
    }

    public void ChangeNetSize()
    {
        for (int i = 0; i < nets.Length; i++)
        {
            nets[i].transform.localScale = Vector3.one * netSilder.value;
        }
    }

    public void ChangeACSize()
    {
        for (int i = 0; i < acs.Length; i++)
        {
            acs[i].transform.localScale = Vector3.one * acSlider.value;
        }
    }
    #endregion
}
