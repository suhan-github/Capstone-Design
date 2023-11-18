using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ImageActivator : MonoBehaviour
{
    public GameObject[] cardImages;
    [SerializeField]protected GameObject[] slots;

    public GameObject SuccessText;
    public GameObject FailText;

    void Start()
    {
        SuccessText.SetActive(false);
        FailText.SetActive(false);
    }

    public void ClickThisButton()
    {
        string dummy_ = "";
        for(int i = 0; i < slots.Length; i++)
        {
            DraggableUI dummy_drag = slots[i].GetComponentInChildren<DraggableUI>();
            if (!ReferenceEquals(dummy_drag, null))
            {
                dummy_ += dummy_drag.content;
            }
        }

        // debug
        Debug.Log(dummy_);

        int dummy_get = CardPanelInitializer.Instance.GetImageIndex(dummy_);
        CardPanelInitializer.Instance.ActivateImage(dummy_get);

        // 글자 없애기
        if (dummy_get >= 0)
        {
            Debug.Log("이미지 일치 Text활성화");

            if(SuccessText != null)
            {
                SuccessText.SetActive(true);
                StartCoroutine(HideSuccessTextAfterDelay(1f));
            }

            for (int i = 0; i < slots.Length; i++)
            {
                DraggableUI dummy_drag = slots[i].GetComponentInChildren<DraggableUI>();
                if (!ReferenceEquals(dummy_drag, null))
                {
                    if (!ReferenceEquals(null, dummy_drag.prev_change_slot))
                    { dummy_drag.prev_change_slot.GetComponent<DroppableUI>().data = null; }
                    if (!ReferenceEquals(null, dummy_drag.prev_slot))
                    {
                        dummy_drag.prev_slot.GetComponent<DroppableUI>().data = null;
                        SlotData dummy_slot = dummy_drag.prev_slot.GetComponent<SlotData>();
                        if (!ReferenceEquals(dummy_slot, null))
                        { dummy_slot.isEmpty = true; }
                    }
                    
                    dummy_drag.gameObject.SetActive(false);
                }
            }
            
        }
        else
        {
            if(FailText != null)
            {
                FailText.SetActive(true);
                StartCoroutine(HideFailTextAfterDelay(1.5f));
            }
        }
    }

    IEnumerator HideSuccessTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (SuccessText != null)
        {
            SuccessText.SetActive(false);
        }
    }

    IEnumerator HideFailTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(FailText != null)
        {
            FailText.SetActive(false);
        }
    }
}
