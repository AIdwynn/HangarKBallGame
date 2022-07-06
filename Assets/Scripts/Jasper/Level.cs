using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private int _levelNumber;


    public void LoadLevel()
    {
        LevelManager.LoadLevel(_levelNumber);
    }
}
