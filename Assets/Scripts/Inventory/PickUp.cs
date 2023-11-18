using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject[] slotItem;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Inventory inven = collision.GetComponent<Inventory>();
            for (int i = 0; i < inven.slots.Count; i++)
            {
                if (inven.slots[i].isEmpty)
                {                   
                    GameObject dummy = Instantiate(slotItem[0], inven.slots[i].slotObj.transform, false);
                    inven.slots[i].isEmpty = false;
                    inven.slots[i].GetComponent<DroppableUI>().data = dummy;
                    DraggableUI dummy_drag = dummy.GetComponent<DraggableUI>();
                    dummy_drag.prev_slot = inven.slots[i].gameObject;
                    dummy_drag.PreviousParent = inven.slots[i].transform;                    
                    Destroy(this.gameObject);
                    
                    break;
                }
            }
            
        }

    }
}
