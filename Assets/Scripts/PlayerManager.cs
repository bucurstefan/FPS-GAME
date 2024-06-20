using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public static int playerHP = 100;
    public TextMeshProUGUI playerHPText;


    public static bool isGameOver;
    void Start()
    {
        isGameOver = false;
        playerHP = 100;
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player has collided with a game object tagged "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10); // Player takes 10 damage
        }
    }

   public void TakeDamage(int damage)
    {
        playerHP -= damage; // Reduce the player's health

        // Check if the player's health has dropped to 0 or below
        if (playerHP <= 0)
        {
            // Here you can handle the player's death, like disabling the game object
            gameObject.SetActive(false);
        }
    }
}
    

