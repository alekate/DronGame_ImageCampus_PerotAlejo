using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyDeath : MonoBehaviour
{
    public ParticleSystem enemyParticle;
    public GameObject enemyVisual;

    private void Awake()
    {
        enemyVisual.SetActive(true);
    }
    public void DeathEnemy()
    {
        enemyVisual.SetActive(false);
        StartCoroutine(PlayParticlesAndDestroy());
    }

    private IEnumerator PlayParticlesAndDestroy()
    {
        if (enemyParticle != null)
        {
            ParticleSystem ps = Instantiate(enemyParticle, enemyParticle.transform.position, enemyParticle.transform.rotation);
            ps.Play();
            Destroy(ps.gameObject, ps.main.duration + 0.5f);
        }

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
