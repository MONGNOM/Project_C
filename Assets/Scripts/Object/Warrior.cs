using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class Warrior : Player
{
    private void Start()
    {
        base.Start();
        boxRange = new Vector2(2, 2);
    }


}
