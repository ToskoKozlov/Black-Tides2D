using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private Text loadingText;

    // Updates once per frame
    void Update()
    {
        // ...then pulse the transparency of the loading text to let the player know that the computer is still working.
        loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
    }
}