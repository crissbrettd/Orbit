using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            Debug.LogWarning("Player left screen");
            SendSceneReset();
        }
    }

    private void SendSceneReset() {
        gameManager.ResetScene();
    }

}
