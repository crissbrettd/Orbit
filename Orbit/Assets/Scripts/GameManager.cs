using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject LevelBounds;

    [SerializeField]
    Rocketv3 PlayerRocket;

    [SerializeField]
    TextMeshProUGUI tmpRevolutions;

    private int numberOfRevolutions = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
      
    }

    public void AddRevolution() {
        numberOfRevolutions += 1;
        tmpRevolutions.text = $"Number of revolutions: {numberOfRevolutions}";
    }

    void OnTriggerExit2D(Collider2D collider) {
      Debug.LogWarning("Player left screen");
    }

    public void ResetScene() {
        SceneManager.LoadScene(0);
    }
}
