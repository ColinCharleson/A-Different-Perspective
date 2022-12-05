using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public int twig;
    public int leaf;
    public int mud;
    public int berry;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.tag = "Grabbable";
    }
}
