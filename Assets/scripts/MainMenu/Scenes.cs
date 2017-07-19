using UnityEngine.SceneManagement;

public class Scenes 
{
    private static string GameScene = "GameView";

    public static void LoadGameView()
    {
       SceneManager.LoadSceneAsync(GameScene);
    }
}