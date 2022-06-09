using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    // The game over scene
    private const int finishedGameSceneId = 3;

    // The items in the ORDER we want to use them
    [SerializeField]
    private Queue<CollectableItem> tasks;

    [SerializeField]
    [Tooltip("The current mission.")]
    private CollectableItem[] missionItems;

    // The current item to collect
    private CollectableItem currentTask => tasks.Peek();

    private const string inventoryItemTag = "Collectable";

    [Tooltip("UI element to write the name of the current collectable item to.")]
    public UnityEngine.UI.Text ClosestItemText;

    [SerializeField]
    [Tooltip("UI text element for the text that shows what item" +
        "is the user supposed to collect next.")]
    private UnityEngine.UI.Text nextItemText;

    [SerializeField]
    [Tooltip("UI text element for the counter")]
    private UnityEngine.UI.Text counterText;

    // data for the counter
    private int soFar = -1;
    private int total = 0;

    // An audio source. The player already has a source
    private AudioSource audioSource;

    void Start()
    {
        // Randomize the editor-provided mission
        Util.Shuffle(missionItems);

        // Init the queue with the mission data
        tasks = new Queue<CollectableItem>(missionItems);

        audioSource = GetComponent<AudioSource>();

        setNextItemUI();
        // Init the counter
        total = tasks.Count;
        updateCounter();
    }

    void updateCounter()
    {
        // Increase the internal counter
        soFar++;

        // Check the counter text is not null
        if (counterText == null) return;

        // Update it because it is not null

        counterText.text = $"Items: {soFar}/{total}";
    }

    private void setNextItemUI()
    {
        // Set the initial element
        if (tasks.Count != 0 && nextItemText != null)
        {
            nextItemText.text = currentTask.LangDependantName;

            Debug.Log("The next item is: " + currentTask.LangDependantName);
        }
    }

    // Set up state machine for collectable items
    private GameObject closestItemGameObject; // For disabling
    private CollectableItem closestItem;

    // Right now the state is can collect true/false
    private bool canInteract = false;

    // The transitions for the state machine are
    // the events when the user enters or exits the area of the
    // collectable item



    // TODO: Add colors and/or icons to the UI

    /// <summary>
    /// Function to add an element to the inventory, and update the HUD.
    /// </summary>
    /// <param name="other">The collider.</param>
    void OnTriggerEnter(Collider other)
    {
        // Handle the collectable item if the tag matches
        // if (other.CompareTag(inventoryItemTag)) handleItem(other);
        // Set the current item as the one that we can interact with
        if (!other.CompareTag(inventoryItemTag)) return;

        // Set this one as the current object

        // Get the Collectable game component,
        // which defines the behaviour and has the data
        var collectable = other.GetComponent<CollectableItem>();

        // It CANNOT be null
        if (collectable == null) return;

        // Here we know it is NOT NULL
        this.closestItemGameObject = other.gameObject;
        this.closestItem = collectable;

        this.canInteract = true;

        Debug.Log($"Can interact with item '{collectable}'");

        if (ClosestItemText != null)
        {
            // Maybe show a custom text depending on the language
            ClosestItemText.text = collectable.LangDependantName;
        }

        // Show the next task
        setNextItemUI();

    }

    // Executed when the player exits a collectable object
    void OnTriggerExit(Collider other)
    {
        // Ensure this is an inventory item using its tag
        if (!other.CompareTag(inventoryItemTag)) return;

        Debug.Log($"Cannot interact with item '{closestItem}'");

        // Set everything to null as we cannot interact
        this.closestItemGameObject = null;
        this.closestItem = null;

        this.canInteract = false;

        // Remove the on screen text
        if (ClosestItemText != null)
        {
            // Maybe show a custom text depending on the language
            ClosestItemText.text = "";
        }
    }

    public void OnCollectItem()
    {
        // Check the queue
        if(tasks.Count != 0)
        {
            Debug.Log($"Task queue len: {tasks.Count}. Top: {tasks.Peek()}");
        }

        if (currentTask != null) Debug.Log("Expected: " + currentTask.LangDependantName);
        // Check we can interact
        if (!canInteract || !closestItem || !closestItemGameObject) 
            return;

        Debug.Log("Can interact");
        // Can interact

        // The queue must NOT be empty
        if(tasks.Count == 0)
        {
            Debug.Log("Empty queue");
            return;
        }

        // Check the list/queue
        // You can only collect the required 
        if (!closestItem.Equals(currentTask))
        {
            // Send a warning to the user.
            Debug.Log("Not the current item.");
            return;
        }

        // Disable the item thru its game object
        // Play the audio
        if(audioSource != null) 
            audioSource.PlayOneShot(closestItem.Pronuntiation);

        closestItemGameObject.SetActive(false);

        // Empty the HUD name display
        if(ClosestItemText == null) return;
        ClosestItemText.text  = "";

        // Update the mission and the hud
        tasks.Dequeue(); // Go to the next task

        // If the queue is empty, the game is over
        if(tasks.Count == 0)
        {
            Debug.Log("Empty queue. The mission is finished");
            SceneManager.LoadScene(finishedGameSceneId);
        }

        setNextItemUI(); // Update the UI
        updateCounter(); // Update the on screen counter
    }


}