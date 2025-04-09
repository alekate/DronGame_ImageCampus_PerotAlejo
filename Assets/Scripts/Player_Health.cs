using UnityEngine.SceneManagement;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 4;
    private bool isDead = false;

    public ParticleSystem sparksParticle;
    public Rigidbody rb;

    [SerializeField] private UIController uiController;

    private void Awake()
    {
        sparksParticle.Pause();
        currentHealth = maxHealth;

        if (uiController != null)
            uiController.UpdateHealth(currentHealth, maxHealth);
    }

    private void Update()
    {
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Dead();
            currentHealth = 0;
        }

        if (isDead && Input.GetKey(KeyCode.R))
        {
            RestartScene();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Ground"))
        {
            if (currentHealth > 0)
            {
                currentHealth -= 1;
                sparksParticle.Play();

                if (uiController != null)
                    uiController.UpdateHealth(currentHealth, maxHealth);
            }
        }
    }

    private void Dead()
    {
        GetComponent<Player_Movement>().enabled = false;
        GetComponent<Player_Rotation>().enabled = false;
        rb.useGravity = true;
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
