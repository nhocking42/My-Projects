using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIButton : MonoBehaviour
{
    public Animator ani;

    void Start()
    {
        ani.SetTrigger("Appear");
    }


}
