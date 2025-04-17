using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Shoot : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask destroyableLayer;

    [SerializeField] float shootRange = 10f;

    [SerializeField] Enemy enemy;

    [SerializeField] UIController uiController;

    public ParticleSystem shootParticle;


    private void Awake() 
    {
        
        if (cam == null)
        {
            cam = Camera.main;
        }

        shootParticle.Pause();

    }

    void Update()
    {
        Shoot();   
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);

            uiController.FlashRedCrosshair();

            shootParticle.Play();

            if (Physics.Raycast(ray, out RaycastHit hit, shootRange, destroyableLayer))
            {
                Enemy hitEnemy = hit.collider.GetComponentInParent<Enemy>();

                if (hitEnemy != null)
                {
                    hitEnemy.TakeDamage(1);
                }
            }
        }
    }


}
