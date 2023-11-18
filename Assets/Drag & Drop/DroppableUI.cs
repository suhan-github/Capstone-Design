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
    /// ���콺 ����Ʈ�� ���� ������ ���� ���� ���η� �� �� 1ȸ ȣ��
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
	{
		// ������ ������ ������ ��������� ����
		//image.color = Color.yellow;
	}

	/// <summary>
	/// ���콺 ����Ʈ�� ���� ������ ���� ������ �������� �� 1ȸ ȣ��
	/// </summary>
	public void OnPointerExit(PointerEventData eventData)
	{
		// ������ ������ ������ �Ͼ������ ����
		//image.color = Color.white;
	}

	/// <summary>
	/// ���� ������ ���� ���� ���ο��� ����� ���� �� 1ȸ ȣ��
	/// </summary>
	public void OnDrop(PointerEventData eventData)
	{
		// pointerDrag�� ���� �巡���ϰ� �ִ� ���(=������)
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
            
			// �巡���ϰ� �ִ� ����� �θ� ���� ������Ʈ�� �����ϰ�, ��ġ�� ���� ������Ʈ ��ġ�� �����ϰ� ����

		}
	}
}

