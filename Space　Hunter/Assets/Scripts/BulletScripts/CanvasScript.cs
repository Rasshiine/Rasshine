using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CanvasScript : MonoBehaviour
{
    [SerializeField] Slider HPSlider;

    // Start is called before the first frame update
    void Start()
    {
        HPSlider.DOValue(3, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        HPSlider.DOValue(3, 2.0f);
    }
}
