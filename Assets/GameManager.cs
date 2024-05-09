using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
public class TextObject
{
    public int id;
    public string text;
}
public class GameManager : MonoBehaviour
{
    public AudioSource audio;
    public Slider volumeSlider;
    public Vector2 panelPos;
    public float panelSpeed;
    public Transform tr;
    public Button[] buttons;

    Vector2 temp;

    void Start()
    {

    }
    public void SetVolume()
    {
        audio.volume = volumeSlider.value;
    }
    public void DisableButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    public void EnableButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }
    // Update is called once per frame
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
        DisableButtons();
        panel.transform.position = panelPos;
        temp = panelPos;
        panelPos = new Vector2(panelPos.x, -panelPos.y);
        StartCoroutine(SlidePanelToPos(panel, false));


    }
    public void ChangeLanguage(int id)
    {
        List<TextObject> list = new List<TextObject>();
        if (id == 0)
        {
            Debug.Log("Language changed to English");
            UnityEngine.TextAsset textAsset = Resources.Load<UnityEngine.TextAsset>("english");
            list = GetTextObjects(textAsset);
        }
        else
        {
            Debug.Log("Language changed to German");
            UnityEngine.TextAsset textAsset = Resources.Load("german") as UnityEngine.TextAsset;
            list = GetTextObjects(textAsset);
            
        }

        List<TextChange> changeList = new List<TextChange>();
        changeList.AddRange(FindObjectsOfType<TextChange>());
        foreach (TextChange textChange in changeList)
        {
            if(textChange.textIndex <= 0)
                textChange.SetText(textChange.text.text);
            else
            textChange.SetText(list.Find(x => x.id == textChange.textIndex).text);
            
        }
    }
    public List<TextObject> GetTextObjects(UnityEngine.TextAsset textAsset)
    {
        List<TextObject> list = new List<TextObject>();
        string[] lines = textAsset.text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string[] line = lines[i].Split(';');
            TextObject textObject = new TextObject();
            textObject.id = int.Parse(line[0]);
            textObject.text = line[1];
            list.Add(textObject);
        }
        return list;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public IEnumerator SlidePanelToPos(RectTransform panel, bool down)
    {
        DisableButtons();
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
        EnableButtons();
    }
    void Update()
    {
        if (tr != null)
            Debug.Log(tr.position);
    }
    
}
