using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private	Transform		canvas;				// UI�� �ҼӵǾ� �ִ� �ֻ���� Canvas Transform
	[SerializeField]private	Transform		previousParent;		// �ش� ������Ʈ�� ������ �ҼӵǾ� �־��� �θ� Transfron
	private	RectTransform	rect;				// UI ��ġ ��� ���� RectTransform
	private	CanvasGroup		canvasGroup;        // UI�� ���İ��� ��ȣ�ۿ� ��� ���� CanvasGroup
	public string content;
	public GameObject prev_slot, prev_change_slot;
	private bool prev_change_mode;

	private void Awake()
	{
		canvas		= FindObjectOfType<Canvas>().transform;
		rect		= GetComponent<RectTransform>();
		canvasGroup	= GetComponent<CanvasGroup>();
		prev_change_mode = GameManager.Instance.is_Change;
		prev_change_slot = null;

	}

    private void Update()
    {
        if (prev_change_mode != GameManager.Instance.is_Change)
		{
			prev_change_mode = GameManager.Instance.is_Change;

            transform.SetParent(previousParent);
            rect.position = previousParent.GetComponent<RectTransform>().position;
            canvasGroup.alpha = 1.0f;
            canvasGroup.blocksRaycasts = true;
        }
    }

    /// <summary>
    /// ���� ������Ʈ�� �巡���ϱ� ������ �� 1ȸ ȣ��
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
	{
		// �巡�� ������ �ҼӵǾ� �ִ� �θ� Transform ���� ����
		if (!GameManager.Instance.is_Change)
		{
			previousParent = transform.parent;
            if (!ReferenceEquals(prev_slot, null))
            { prev_slot.GetComponent<DroppableUI>().data = null; }
        }

		// ���� �巡������ UI�� ȭ���� �ֻ�ܿ� ��µǵ��� �ϱ� ����
		transform.SetParent(canvas);		// �θ� ������Ʈ�� Canvas�� ����
		transform.SetAsLastSibling();		// ���� �տ� ���̵��� ������ �ڽ����� ����

		// �巡�� ������ ������Ʈ�� �ϳ��� �ƴ� �ڽĵ��� ������ ���� ���� ������ CanvasGroup���� ����
		// ���İ��� 0.6���� �����ϰ�, ���� �浹ó���� ���� �ʵ��� �Ѵ�
		canvasGroup.alpha = 0.6f;
		canvasGroup.blocksRaycasts = false;
	}

	/// <summary>
	/// ���� ������Ʈ�� �巡�� ���� �� �� ������ ȣ��
	/// </summary>
	public void OnDrag(PointerEventData eventData)
	{
		// ���� ��ũ������ ���콺 ��ġ�� UI ��ġ�� ���� (UI�� ���콺�� �Ѿƴٴϴ� ����)
		rect.position = eventData.position;
	}

	/// <summary>
	/// ���� ������Ʈ�� �巡�׸� ������ �� 1ȸ ȣ��
	/// </summary>
	public void OnEndDrag(PointerEventData eventData)
	{
        // �巡�׸� �����ϸ� �θ� canvas�� �����Ǳ� ������
        // �巡�׸� ������ �� �θ� canvas�̸� ������ ������ �ƴ� ������ ����
        // ����� �ߴٴ� ���̱� ������ �巡�� ������ �ҼӵǾ� �ִ� ������ �������� ������ �̵�
        transform.SetParent(previousParent);
        rect.position = previousParent.GetComponent<RectTransform>().position;
        // �������� �ҼӵǾ��־��� previousParent�� �ڽ����� �����ϰ�, �ش� ��ġ�� ����
        if (GameManager.Instance.is_Change)
		{
			if (!ReferenceEquals(null, prev_change_slot))
			{
				transform.SetParent(prev_change_slot.transform);
				rect.position = prev_change_slot.GetComponent<RectTransform>().position;
			}
		}


		// ���İ��� 1�� �����ϰ�, ���� �浹ó���� �ǵ��� �Ѵ�
		canvasGroup.alpha = 1.0f;
		canvasGroup.blocksRaycasts = true;
	}

    public Transform PreviousParent { get { return previousParent; } set { previousParent = value; } }
}

