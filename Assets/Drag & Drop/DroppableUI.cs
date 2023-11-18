using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableUI : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
	private	Image			image;
	private	RectTransform	rect;
	public GameObject data, prev_data;
    private bool prev_change_mode;


    private void Awake()
	{
		image	= GetComponent<Image>();
		rect	= GetComponent<RectTransform>();
		data = null;
		prev_data = null;
	}
    private void Update()
    {
        if (prev_change_mode != GameManager.Instance.is_Change)
        {
            prev_change_mode = GameManager.Instance.is_Change;

			data = prev_data;
        }
    }
    /// <summary>
    /// 마우스 포인트가 현재 아이템 슬롯 영역 내부로 들어갈 때 1회 호출
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
	{
		// 아이템 슬롯의 색상을 노란색으로 변경
		//image.color = Color.yellow;
	}

	/// <summary>
	/// 마우스 포인트가 현재 아이템 슬롯 영역을 빠져나갈 때 1회 호출
	/// </summary>
	public void OnPointerExit(PointerEventData eventData)
	{
		// 아이템 슬롯의 색상을 하얀색으로 변경
		//image.color = Color.white;
	}

	/// <summary>
	/// 현재 아이템 슬롯 영역 내부에서 드롭을 했을 때 1회 호출
	/// </summary>
	public void OnDrop(PointerEventData eventData)
	{
		// pointerDrag는 현재 드래그하고 있는 대상(=아이템)
		if ( eventData.pointerDrag != null && ReferenceEquals(null, data) )
		{
			if (!GameManager.Instance.is_Change)
			{
				eventData.pointerDrag.GetComponent<DraggableUI>().PreviousParent = this.transform;

                data = eventData.pointerDrag.transform.gameObject;
				this.prev_data = data;
				DraggableUI data_d = eventData.pointerDrag.GetComponent<DraggableUI>();
                if (!ReferenceEquals(data_d.prev_slot, null)) { 
					SlotData dummy_slot = data_d.prev_slot.GetComponent<SlotData>();
					data_d.prev_slot.GetComponent<DroppableUI>().prev_data = null;
					if (!ReferenceEquals(dummy_slot, null))
						dummy_slot.isEmpty = true;
					SlotData dummy_slot2 = this.GetComponent<SlotData>();
					if (!ReferenceEquals(dummy_slot2, null))
					{ dummy_slot2.isEmpty = false; }

                    eventData.pointerDrag.transform.SetParent(transform);
                    eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
                    data_d.prev_slot = this.gameObject;
                }
               
				
			}
			else
			{
				if (ReferenceEquals(this.GetComponent<SlotData>(), null))
				{
					this.data = eventData.pointerDrag.gameObject;
                    eventData.pointerDrag.transform.SetParent(transform);
                    eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;

					DraggableUI dummy_drag = data.GetComponent<DraggableUI>();
					if (!ReferenceEquals(dummy_drag.prev_change_slot, null))
					{ dummy_drag.prev_change_slot.GetComponent<DroppableUI>().data = null; }

					dummy_drag.prev_change_slot = this.gameObject;
                }

			}
            
			// 드래그하고 있는 대상의 부모를 현재 오브젝트로 설정하고, 위치를 현재 오브젝트 위치와 동일하게 설정

		}
	}
}

