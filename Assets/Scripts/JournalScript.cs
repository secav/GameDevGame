using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JournalScript : MonoBehaviour
{
    public string journalText = "";
    public float fontSize = 5f;
    public Color fontColor = Color.black;

    private GameObject openJournal;
    private GameObject closedJournal;
    private GameObject textMesh;
    private TextMeshPro textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        openJournal = transform.Find("Open").gameObject;
        closedJournal = transform.Find("Closed").gameObject;
        textMesh = transform.Find("Journal Text").gameObject;
        textMeshPro = textMesh.GetComponent<TextMeshPro>();

        textMesh.SetActive(false);
        closedJournal.SetActive(true);
        openJournal.SetActive(false);
        textMeshPro.text = journalText;
        textMeshPro.fontSize = fontSize;
        textMeshPro.color = fontColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            // Enable the Open child and disable the Closed child
            textMesh.SetActive(true);
            openJournal.SetActive(true);
            closedJournal.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        textMesh.SetActive(false);
        openJournal.SetActive(false);
        closedJournal.SetActive(true);        
    }
}
