using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickNewGameAnimal()
    {
        //SceneManager.LoadScene(1);
        LoadingSceneController.Instance.LoadScene("PolyPark(Animal)");

    }

    public void OnClickNewGameFruit()
    {
        //SceneManager.LoadScene(2);
        LoadingSceneController.Instance.LoadScene("PolyPark(Fruit)");
    }

    public void OnClickLoad()
    {
        Debug.Log("불러오기");
    }

    public void OnClickOption()
    {
        Debug.Log("옵션");
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

    public void OnClickCollection()
    {
        Debug.Log("컬렉션 선택");
    }
}
