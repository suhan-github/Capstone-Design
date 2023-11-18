using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<SlotData> slots = new List<SlotData>();
    private int maxSlot = 15;
    public GameObject slotPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        GameObject slotPanel = GameObject.Find("InvenPanel");
        
        for(int i = 0; i < maxSlot; i++)
        {
            GameObject go = Instantiate(slotPrefab, slotPanel.transform, false);
            go.name = "Slot_" + i;
            SlotData slot = go.GetComponent<SlotData>();
            slot.isEmpty = true;
            slot.slotObj = go;
            slots.Add(slot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
