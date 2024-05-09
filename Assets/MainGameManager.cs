using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public Vector2 panelPos;
    public float panelSpeed;
    Transform tr;
    Vector2 temp;
    Button[] buttons;
    public RectTransform pauseMenu;
    bool panelMoving;
    bool up;
    public void DisableButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }
    public void EnableButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }
    public void ShowPanel(RectTransform panel)
    {
        panel.transform.position = new Vector2(panelPos.x, -panelPos.y);
        tr = panel.transform;
        temp = panelPos;
        Debug.Log(panel.transform.position);
        panel.gameObject.SetActive(true);
        StartCoroutine(SlidePanelToPos(panel, true));
    }
    public void HidePanel(RectTransform panel)
    {

        panel.transform.position = panelPos;
        temp = panelPos;
        panelPos = new Vector2(panelPos.x, -panelPos.y);
        StartCoroutine(SlidePanelToPos(panel, false));


    }
    public IEnumerator SlidePanelToPos(RectTransform panel, bool down)
    {
        panelMoving = true;
        yield return new WaitForEndOfFrame();
        Transform panelTransform = panel.transform;
        while (Vector2.Distance(panelTransform.position, panelPos) > 0.1f)
        {
            panelTransform.position = Vector2.Lerp(panelTransform.position, panelPos, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
        panelPos = temp;
        temp = Vector2.zero;
        panel.gameObject.SetActive(down);
        panelMoving = false;

    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && !panelMoving)
        {
            if (!up)
            {
                ShowPanel(pauseMenu);
                up = true;
            }
            else
            {
                HidePanel(pauseMenu);
                up = false;
            }
        }
    }
}
