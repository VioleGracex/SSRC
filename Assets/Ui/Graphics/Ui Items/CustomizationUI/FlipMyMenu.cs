using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipMyMenu : MonoBehaviour
{
    public void FlipActivity(GameObject menu)
    {
        menu.SetActive(!menu.activeSelf);
    }
}
