using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftTimer : MonoBehaviour
{
    private Slider timerSlider;
    public int maxValue = 0;
    private float currentTime = 0f;
    private bool isActive = false;

    void Start()
    {
        timerSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (maxValue > 0)
        {
            if(!isActive)
            {
                isActive = true;
                CoroutineManager.Instance.StartCoroutineUnstoppable(UpdateSliderCoroutine());
            }
        }
    }

    IEnumerator UpdateSliderCoroutine()
    {
        while (currentTime < maxValue)
        {
            currentTime += Time.deltaTime;
            float fillAmount = currentTime / maxValue;
            timerSlider.value = fillAmount;
            yield return null;
        }

        Destroy(gameObject);
    }
}
