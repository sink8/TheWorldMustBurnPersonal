using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferWater : MonoBehaviour
{
    // Start is called before the first frame updatepublic Transform waterBoxA; // The one that lowers
    public Transform waterBoxA; // The one that rises
    public Transform waterBoxB; // The one that rises
    [SerializeField] Animator animator_fire;

    [SerializeField] Collider2D col;
    [SerializeField] Collider2D col2;

     float minHeight = 0.0f;
    public float maxHeight = 3f;
    public float transferSpeed = 1f;

    public float heightTop;
    public float heightBottom;
    public bool isTransferring = false;
    public ParticleSystem particless;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        heightTop = waterBoxA.localScale.y;
        heightBottom = waterBoxB.localScale.y;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        // Trigger example
        if (isTransferring) {
            float delta = transferSpeed * Time.deltaTime;
            print("is 1");
            // Transfer from top (A) to bottom (B)
            if (heightTop > minHeight && heightBottom < maxHeight) {
                heightTop -= delta;
                heightBottom += delta;

                ApplyHeight(waterBoxA, heightTop);   // Top goes down
                ApplyHeight(waterBoxB, heightBottom); // Bottom goes up

                // Update values
                heightTop = waterBoxA.localScale.y;
                heightBottom = waterBoxB.localScale.y;
            } else {
                // The transfer is finished: hide the box and stop transferring
                waterBoxA.gameObject.SetActive(false);
                isTransferring = false;
            }
        }
    }

    void ApplyHeight(Transform box, float height)
    {
         print("is 3");
        Vector3 scale = box.localScale;
        scale.y = height;
        box.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Palo"))
        {
            particless.Play();
            AudioFW.Play("HitEnemyWater");
            isTransferring = true;
            animator_fire.Play("FireUpNewAnimation");
            col2.enabled = true;
            col.enabled = false;
            spriteRenderer.enabled = false;
            Destroy(particless, 2);
        }
    }
}
