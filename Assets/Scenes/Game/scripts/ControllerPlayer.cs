using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class ControllerPlayer : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    public Image[] hearts;
    private Animator animator;
    public AudioClip damageSound;  // Sonido de da�o
    public AudioClip deathSound;   // Sonido de muerte
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Obt�n el componente AudioSource
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        Debug.Log($"Player recibi� da�o. Vida restante: {currentHealth}");

        // Reproduce sonido de da�o
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);  // Reproduce el sonido de da�o
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentHealth;
        }
    }

    private void Die()
    {
        Debug.Log("Player ha muerto.");

        // Reproduce sonido de muerte
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);  // Reproduce el sonido de muerte
        }

        StartCoroutine(PlayDeathAnimation());
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            //StartCoroutine(RecibirDa�o());
        }
    }
    private IEnumerator PlayDeathAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die");
            yield return new WaitForSeconds(1.5f);
        }
        SceneManager.LoadScene("GameOver");
    }
}
