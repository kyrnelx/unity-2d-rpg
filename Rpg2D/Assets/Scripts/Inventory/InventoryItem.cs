using System;

[Serializable]
public class InventoryItem
{
    public int number;
    public InventoryItemBaseData itemSO;

    public InventoryItem(InventoryItemBaseData itemSO)
    {
        this.itemSO = itemSO;
        number = 1;
    }

    public InventoryItem(InventoryItemBaseData itemSO, int number)
    {
        this.itemSO = itemSO;
        this.number = number;
    }

    public void Add()
    {
        number++;
    }

    public void Add(int number)
    {
        this.number += this.number;
    }

    public void Remove()
    {
        if (number > 0) number--;
    }

    public void Remove(int number)
    {
        this.number -= number;
        if (this.number < 0) this.number = 0;
    }
}