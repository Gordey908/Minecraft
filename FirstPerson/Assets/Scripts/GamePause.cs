using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    public static bool isPause;
    [SerializeField]
    private GameObject[] UIInteface;
    [SerializeField]
    private GameObject PausePanel;
    [SerializeField]
    private Slider cameraViewSlider;
    [SerializeField]
    private Slider cameraViewSlider;


    private void Start()
    {
        cameraViewSlider.value = cameraViewSlider.main.farClipPlane;
        foreach (GamePause )
        {

        }
    }
}
