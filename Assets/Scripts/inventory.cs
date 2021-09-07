using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this was going to be used but woul require a rework of how the items are stored and handled, i dont think i have time to do this but ill leave the code here
/// from https://www.youtube.com/watch?v=2WnAOV7nHW0
/// instead of this i implemented the "item command" this gives the description and stats for all items.
/// sad that my *amazing* art was wasted
/// </summary>
public class inventory : MonoBehaviour
{
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    void Awake(){
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = transform.Find("ItemSlotTemplate");
    }

    public void refreshIvn() {
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 30f;
        foreach (string item in itemManager.items) {
            RectTransform itemSlotTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotTransform.gameObject.SetActive(true);
            itemSlotTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            x++;
            if (x > 4) {
                x = 0;
                y++;
            }
        }
    }
}
