using System.Collections;
using UnityEngine;

public class Player_Shoot : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask destroyableLayer;
    [SerializeField] private float shootRange = 10f;
    [SerializeField] private Enemy enemy;
    [SerializeField] private UIController uiController;
    [SerializeField] private Player_CamaraChanger playerCamaraChanger;
    [SerializeField] Transform shootOrigin;


    public GameObject bulletImpact;
    public float fireRate = 5f;
    private float nextTimeToFire = 0f;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float lineDuration = 0.05f;

    private void Awake()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            Ray ray;

            if (playerCamaraChanger.isFirstPerson)
            {
                ray = cam.ScreenPointToRay(Input.mousePosition);
            }
            else
            {
                ray = new Ray(shootOrigin.position, shootOrigin.forward);
            }

            uiController.FlashRedCrosshair();

            if (Physics.Raycast(ray, out RaycastHit hit, shootRange, destroyableLayer))
            {
                Enemy hitEnemy = hit.collider.GetComponentInParent<Enemy>();

                if (hitEnemy != null)
                {
                    hitEnemy.TakeDamage(1);
                }

                Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.normal));

                if (playerCamaraChanger.isFirstPerson)
                {
                    StartCoroutine(ShowLine(shootOrigin.position, hit.point));
                }
            }

            nextTimeToFire = Time.time + 1f / fireRate;
        }
    }

    IEnumerator ShowLine(Vector3 start, Vector3 end)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        yield return new WaitForSeconds(lineDuration);

        lineRenderer.enabled = false;
    }

}
