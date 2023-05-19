using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestStart : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshPro a;
    void Start()
    {
        CommandQueue.GetInstance.AddCommand(new VanishingImageHorizontally(a.rectTransform), true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
