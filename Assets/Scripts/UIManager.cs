using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set;}
    #region VARIABLES
    [SerializeField] private TextMeshProUGUI scoreText;


    #endregion

    private void Awake()
    {
        if (Instance != null) {
            Debug.LogError("There is more than 1 Instance of UIManager"); 
        }
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
