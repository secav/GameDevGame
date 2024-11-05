using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string targetScene = "Intro Scene";
    private bool isOpen;
    public Sprite openTop;
    public Sprite openBottom;
    public SpriteRenderer top;
    public SpriteRenderer bottom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && isOpen)
        {
            SceneManager.LoadScene(targetScene);
        }
    }

    public void openDoor()
    {
        isOpen = true;
        top.sprite = openTop;
        bottom.sprite = openBottom;
    }
}
