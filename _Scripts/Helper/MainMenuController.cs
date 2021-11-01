using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject characterSelectPanel;
    public AudioClip clickSound;

    public void StartMissionOne()
    {
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        SceneManager.LoadScene(TagManager.LEVEL_1_NAME);
    }

    public void StartMissionTwo()
    {
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        SceneManager.LoadScene(TagManager.LEVEL_2_NAME);
    }

    public void StartMissionThree()
    {
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        SceneManager.LoadScene(TagManager.LEVEL_3_NAME);
    }

    public void StartMissionFour()
    {
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        SceneManager.LoadScene(TagManager.LEVEL_4_NAME);
    }

    public void OpenCharacterSelectpanel()
    {
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        characterSelectPanel.SetActive(true);
    }

    public void CloseCharacterSelectpanel()
    {
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        characterSelectPanel.SetActive(false);
    }

    public void SelectTommy()
    {
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        GameManager.instance.character_Index = 0;
    }

    public void SelectMarry()
    {
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        GameManager.instance.character_Index = 1;
    }
}
