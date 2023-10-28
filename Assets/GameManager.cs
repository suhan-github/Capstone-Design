using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject menuSet;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //sub menu
        if (Input.GetButtonDown("Cancel"))
        {
            if(menuSet.activeSelf)
                menuSet.SetActive(false);
            else
                menuSet.SetActive(true);
        }
            
    }

    public void OnClickSMenu()
    {
        //sub menu
        menuSet.SetActive(true);
    }

    public void GameSave()
    {
        // score
        // position
        // collection
        PlayerPrefs.SetFloat("PlayerX", Player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", Player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", Player.transform.position.z);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }

    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX")) ;
        return;

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        float z = PlayerPrefs.GetFloat("PlayerZ");

        Player.transform.position = new Vector3(x, y, z);
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
