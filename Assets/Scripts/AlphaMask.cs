using UnityEngine;
using UnityEngine.UI;

public class AlphaMask : MonoBehaviour
{
    [Range(0f, 1f)]
    private float AlphaLevel = 1f;
    private Image bt;

    //Запрещает нажатие кнопки в прозрачной области спрайта
    void Start()
    {
        bt = gameObject.GetComponent<Image>();
        bt.alphaHitTestMinimumThreshold = AlphaLevel;
    }
}