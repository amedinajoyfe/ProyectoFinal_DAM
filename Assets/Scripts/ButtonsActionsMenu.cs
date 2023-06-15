using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonsActionsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text FeedbackScreenMode;
    [SerializeField] private TMP_Text FeedbackScreenResolution;

    public AudioSource MenuSong;

    private int PosMode = 0;
    private int PosRes = 0;
    public void ChangeWindowModeLeft()
    {
        PosMode = PosMode == 0 ? 2 : PosMode -= 1; //If pos equal to 0 then go to 2, else subtract 1
        FeedbackScreenMode.text = StaticVariablesMenu.Instance.ScreenMode.ElementAt(PosMode).Key;
#if !UNITY_EDITOR
        Screen.fullScreenMode = ScreenMode.ElementAt(PosMode).Value;
#endif
    }

    public void ChangeWindowModeRight()
    {
        PosMode = PosMode == 2 ? 0 : PosMode += 1; //If pos equal to 2 then go to 0, else add 1
        FeedbackScreenMode.text = StaticVariablesMenu.Instance.ScreenMode.ElementAt(PosMode).Key;
#if !UNITY_EDITOR
        Screen.fullScreenMode = ScreenMode.ElementAt(PosMode).Value;
#endif
    }

    public void ChangeScreenResolutionLeft()
    {
        PosRes = PosRes == 0 ? 4 : PosRes -= 1;
        FeedbackScreenResolution.text = StaticVariablesMenu.Instance.ScreenResolution.ElementAt(PosRes).Key;
#if !UNITY_EDITOR
        Screen.SetResolution(ScreenResolution.ElementAt(PosRes).Value[0], ScreenResolution.ElementAt(PosRes).Value[1], Screen.fullScreenMode);
#endif
    }

    public void ChangeScreenResolutionRight()
    {
        PosRes = PosRes == 4 ? 0 : PosRes += 1;
        FeedbackScreenResolution.text = StaticVariablesMenu.Instance.ScreenResolution.ElementAt(PosRes).Key;
#if !UNITY_EDITOR
        Screen.SetResolution(ScreenResolution.ElementAt(PosRes).Value[0], ScreenResolution.ElementAt(PosRes).Value[1], Screen.fullScreenMode);
#endif
    }

    public void ChangeVolume(Slider slider)
    {
        MenuSong.volume = slider.value;
    }

    public void ExitGame()
    {
        if(Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }

    public void GoToPlayScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
