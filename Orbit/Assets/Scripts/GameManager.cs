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
    Rocketv2 PlayerRocket;

    [SerializeField]
    TextMeshProUGUI tmpRevolutions;

    private int numberOfRevolutions = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfRevolutions >= 5) {
            ResetScene();
        }
    }

    public void AddRevolution() {
        numberOfRevolutions += 1;
        tmpRevolutions.text = $"Number of revolutions: {numberOfRevolutions}";
    }

    void OnTriggerExit2D(Collider2D collider) {
        // if (collider.gameObject.tag == "Player") {
            Debug.LogWarning("Player left screen");
        // }
    }

    public void ResetScene() {
        SceneManager.LoadScene(0);
    }
}
