using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboText : MonoBehaviour
{
    Text text;

    public int currentCombo = 0;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        text.text = currentCombo.ToString();
    }

    public void NewCombo()
    {
        currentCombo++;

        text.text = currentCombo.ToString();
        transform.localScale = new Vector3(4, 4, 1);
    }


    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 0.2f);
    }
}
