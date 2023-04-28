using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject[] itemSlots; // The empty slots where items can be placed
    public GameObject[] itemPrefabs;  // Array of item prefabs to generate
    public GameObject parentObject;   // Parent object to contain the generated items

    private GameObject[] items;       // Array of items generated for the current round
    private bool[] occupiedSlots;     // Array of flags to indicate if a slot is occupied
    private int addedItemIndex;       // Index of the item that will be added in the second scene

    // Generate the items for a new round
    public void GenerateItems(int itemCount)
    {
        items = new GameObject[itemCount];
        addedItemIndex = Random.Range(0, itemCount);
        
        for (int i = 0; i < itemCount; i++)
        {
            GameObject newItem = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)]);
            newItem.transform.SetParent(parentObject.transform);
            items[i] = newItem;
        }

        // items = new GameObject[itemCount];
        // // Initialize the occupied slots array with false values
        // occupiedSlots = new bool[itemSlots.Length];
        // for (int i = 0; i < occupiedSlots.Length; i++)
        // {
        //     occupiedSlots[i] = false;
        // }

        // // Generate items
        // for (int i = 0; i < itemCount; i++)
        // {
        //     // Find an unoccupied slot
        //     int randomSlot;
        //     do
        //     {
        //         randomSlot = Random.Range(0, itemSlots.Length);
        //     } while (occupiedSlots[randomSlot]);

        //     // Set the slot as occupied
        //     occupiedSlots[randomSlot] = true;

        //     // Set the item's position to the position of the slot
        //     GameObject newItem = Instantiate(itemPrefabs[0], itemSlots[randomSlot].transform.position, Quaternion.identity);
        //     newItem.transform.SetParent(itemSlots[randomSlot].transform);
        //     items[i] = newItem;
        // }
    }

    // Get the index of the item that will be added in the second scene
    public int GetAddedItemIndex()
    {
        return addedItemIndex;
    }

    // Get the array of items generated for the current round
    public GameObject[] GetItems()
    {
        return items;
    }
}
