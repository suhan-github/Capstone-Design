using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public enum BackGround
{
    animal,
    fruit,
}

public enum Size
{
    POT64,
    POT128,
    POT256,
    POT512,
    POT1024,
}

public class Capture : MonoBehaviour
{
    public Camera cam;
    public RenderTexture rt;
    public Image bg;

    public BackGround backGround;
    public Size size;

    public GameObject[] obj;
    int nowcnt = 0;

    private void Start()
    {
        cam = Camera.main;
        SettingColor();
        SettingSize();
    }

    public void Create()
    {
        StartCoroutine(CaptureImage());
    }

    IEnumerator CaptureImage()
    {
        yield return null;

        Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false, true);
        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);

        yield return null;

        var data = tex.EncodeToPNG();
        string name = "Card";
        string extention = ".png";
        string path = Application.persistentDataPath + "/" + name;

        Debug.Log(path);

        if(!Directory.Exists(path)) Directory.CreateDirectory(path);

        File.WriteAllBytes(path + name + extention, data);

        yield return null;
    }

    public void AllCreate()
    {
        StartCoroutine(AllCaptureImage());
    }

    IEnumerator AllCaptureImage()
    {
        while(nowcnt < obj.Length)
        {
            var nowobj = Instantiate(obj[nowcnt].gameObject);

            yield return null;

            Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false, true);
            RenderTexture.active = rt;
            tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);

            yield return null;

            var data = tex.EncodeToPNG();
            string name = $"Card_{ obj[nowcnt].gameObject.name}";
            string extention = ".png";
            string path = Application.persistentDataPath + "/" + name;

            Debug.Log(path);

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            File.WriteAllBytes(path + name + extention, data);

            yield return null;

            DestroyImmediate(nowobj);
            nowcnt++;

            yield return null;
        }
    }

    void SettingColor()
    {
        switch(backGround)
        {
            case BackGround.animal:
                cam.backgroundColor = Color.white;
                bg.color = Color.white;
                break;
            case BackGround.fruit:
                cam.backgroundColor = Color.grey;
                bg.color = Color.grey;
                break;
            default:
                break;
        }
    }

    void SettingSize()
    {
        switch(size)
        {
            case Size.POT64:
                rt.width = 64;
                rt.height = 64;
                break;
            case Size.POT128:
                rt.width = 128;
                rt.height = 128;
                break;
            case Size.POT256:
                rt.width = 256;
                rt.height = 256;
                break;
            case Size.POT512:
                rt.width = 512;
                rt.height = 512;
                break;
            case Size.POT1024:
                rt.width = 1024;
                rt.height = 1024;
                break;
            default:
                break;
        }
    }
}
