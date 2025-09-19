using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartScene()
    {
        SceneManager.LoadScene("SampleScene");
    }  
}
