using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public Text dialogText;

    string[] dialogSequenz = new string[11] { 
        "Thank you for accepting this dangerous mission.",
        "The protection of the power crystal is of up most importance to us all.",
        "This is especially true for the current situation.",
        "Those red headed cubes just don't know how important it is.",
        "I have a gun and a sword for you.",
        "Move with WASD or the arrow keys.",
        "Shoot the gun with the left mouse button and swing the sword with the right one.",
        "Maybe you'll be able to get into the flow.",
        "When you are ready for the flow just press LEFT SHIFT.",
        "You'll slice through them like no one else.",
        "Good luck! I have some other important business to deal with now."};

    int dialogTextIndex;

    bool currentlyScrollingText = false;

    GameStateController gameStateController;

    void Start()
    {
        gameStateController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStateController>();
        StartCoroutine("ScrollText");
    }


    public void Next()
    {
        if (currentlyScrollingText)
        {
            StopCoroutine("ScrollText");
            dialogText.text = dialogSequenz[dialogTextIndex];
            currentlyScrollingText = false;
        }

        if (dialogTextIndex + 1 >= dialogSequenz.Length)
        {
            gameStateController.StartGame();
            Destroy(gameObject);
            return;
        }

        dialogTextIndex++;
        StartCoroutine("ScrollText");

    }

    IEnumerator ScrollText()
    {
        currentlyScrollingText = true;
        dialogText.text = "";

        for (int currentDialogTextIndex = 0; currentDialogTextIndex < dialogSequenz[dialogTextIndex].Length; currentDialogTextIndex++)
        {
            dialogText.text += dialogSequenz[dialogTextIndex][currentDialogTextIndex];
            yield return 0;
        }

        currentlyScrollingText = false;
    }
}
