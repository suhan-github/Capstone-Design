using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    private static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    public static GameManager Instance { get { return instance; } }
    #endregion


    public GameObject menuSet;
    public GameObject Player;
    public GameObject ChangePanel;
    public GameObject CollectionPanel;
    public bool is_Change;

    // Start is called before the first frame update
    void Start()
    {
        is_Change = false;
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

    public void ChangePanel_Active()
    {
        is_Change = !ChangePanel.activeSelf;
        StartCoroutine(PanelActive());
    }

    public void CollectionPanel_Active()
    {
        is_Change = !CollectionPanel.activeSelf;
        StartCoroutine(CollectionPanelActive());
    }

    private IEnumerator PanelActive()
    {
        yield return new WaitForSeconds(0.1f);
        ChangePanel.SetActive(!ChangePanel.activeSelf);
    }

    private IEnumerator CollectionPanelActive()
    {
        yield return new WaitForSeconds(0.1f);
        CollectionPanel.SetActive(!CollectionPanel.activeSelf);
    }


}
