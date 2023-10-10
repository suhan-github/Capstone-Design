using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Select : MonoBehaviour
{
    public GameObject creat;
    public Text[] slotText;
    public Text nwePlayerName;

    bool[] savefile = new bool[3];
    // Start is called before the first frame update
    void Start()
    {
        // 슬롯별로 저장된 데이터가 존재하는지 판단.
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))
            {
                savefile[i] = true;
                DataManager.instance.nowSlot = i;
                DataManager.instance.LoadDate();
                if (i < slotText.Length)
                {
                    slotText[i].text = DataManager.instance.nowPlayer.name;
                }
            }
            else
            {
                // 유효한 인덱스 범위 내에서만 액세스
                if (i < slotText.Length)
                {
                    slotText[i].text = "비어있음";
                }
            }
        }
        DataManager.instance.DataClear();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Slot(int number)
    {
        DataManager.instance.nowSlot = number;

        if (savefile[number])
        {
            DataManager.instance.LoadDate();
            GoGame();
        }
    }

    public void Creat()
    {
        creat.gameObject.SetActive(true);
    }

    public void GoGame()
    {
        if (!savefile[DataManager.instance.nowSlot])
        {
            DataManager.instance.nowPlayer.name = nwePlayerName.text;
            DataManager.instance.SaveDate();
        }
        LoadingSceneController.Instance.LoadScene("PolyPark(Animal)");
    }
}
