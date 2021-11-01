using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public GameObject[] characters;

 
    public int character_Index;
    private void Awake()
    {
        MakeInstance();
    }
    void Start()
    {
        character_Index = 0;
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // Update is called once per frame
 

    void MakeInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        if(scene.name != TagManager.MAIN_MENU_NAME)
        {
            if(character_Index != 0)
            {
                GameObject tommy = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG);

                Instantiate(characters[character_Index], tommy.transform.position, Quaternion.identity);

                tommy.SetActive(false);
            }
        }
    }
}
