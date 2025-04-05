using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] TextMeshProUGUI score;
    void Start()
    {
        score.text = $"{0}";
    }

    
    void Update()
    {
        
    }
}
