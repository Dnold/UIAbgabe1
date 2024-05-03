using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource audio;
    public Slider volumeSlider;
    public Vector2 panelPos;
    public float panelSpeed;

    void Start()
    {
        
    }
    public void SetVolume()
    {
        audio.volume = volumeSlider.value;
    }
    // Update is called once per frame
    public void ShowPanel(RectTransform panel)
    {
        panel.transform.position = new Vector2(panelPos.x, -panelPos.y);
        Debug.Log(panel.transform.position);
        panel.gameObject.SetActive(true);
        StartCoroutine(SlidePanelToPos(panel));
    }
    public IEnumerator SlidePanelToPos(RectTransform panel)
    {
        
        Transform panelTransform = panel.transform;
        while (Vector2.Distance(panelTransform.position, panelPos) > 0.1f)
        {
            panelTransform.position = Vector2.Lerp(panelTransform.position, panelPos, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
    }
    void Update()
    {
        
    }
}
