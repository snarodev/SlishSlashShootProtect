using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowKey : MonoBehaviour
{
    Direction direction;

    bool activeFlowKey = false;

    public FlowController flowController;

    public void Start()
    {
        this.direction = (Direction)Random.Range (0,3);

        if (direction == Direction.Down)
            transform.eulerAngles = new Vector3(0, 0, 180);
        else if (direction == Direction.Up)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (direction == Direction.Left)
            transform.eulerAngles = new Vector3(0, 0, 90);
        else if (direction == Direction.Right)
            transform.eulerAngles = new Vector3(0, 0, 270);
    }

    private void Update()
    {
        if (transform.GetSiblingIndex() == 0)
        {
            activeFlowKey = true;
            GetComponent<Image>().color = Color.red;
        }
            

        if (!activeFlowKey)
            return;

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (direction == Direction.Down)
                flowController.NextFlowKey();
            else
                flowController.ExitFlow(false);
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (direction == Direction.Up)
                flowController.NextFlowKey();
            else
                flowController.ExitFlow(false);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (direction == Direction.Left)
                flowController.NextFlowKey();
            else
                flowController.ExitFlow(false);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (direction == Direction.Right)
                flowController.NextFlowKey();
            else
                flowController.ExitFlow(false);
        }
       
    }

    public enum Direction
    {
        Up,
        Down,
        Right,
        Left
    }
}
