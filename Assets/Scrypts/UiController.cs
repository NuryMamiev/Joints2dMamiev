using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public PlayerMovement playerClass;
    [SerializeField] Image image;
    [SerializeField] Button Button;
    private bool finished = false;
    public bool Finished {  get { return finished; }set { finished = value; } }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void ShowUi()
    {
        image.gameObject.SetActive(!Finished);
        Button.gameObject.SetActive(true);
    }
}
