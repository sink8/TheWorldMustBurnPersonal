using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBurning : MonoBehaviour
{
    public GameObject firePrefab; // Assign the fire prefab in the Inspector
    public Transform startPoint;  // The starting point of the fire on the fuse
    public Transform endPoint;    // The end point of the fire on the fuse
    public float burnDuration = 5f; // Total time for the fire to travel from start to end
    

    private bool isBurning = false;
    private GameObject fireInstance;
    private float burnTime = 0f;
    private float disTime = 0f;

    public Renderer ren;
    Material mat;
    void Start()
    {
        fireInstance = Instantiate(firePrefab, startPoint.position, Quaternion.identity);
        fireInstance.SetActive(false);

        mat = ren.material;
        

    }

    // Update is called once per frame
    void Update()
    {
        IsBurning();
        FuseDissolve();
    }

    void IsBurning()
    {
        if (isBurning)
        {
            burnTime += Time.deltaTime;
            float t = burnTime / burnDuration;
            fireInstance.transform.position = Vector3.Lerp(startPoint.position, endPoint.position, t);

            if (t >= 1f)
            {
                isBurning = false;
                fireInstance.SetActive(false);
                burnTime = 0f;
            }
        }
    }

    private void FuseDissolve()
    {
        if (isBurning)
        {
           
            
                disTime += Time.deltaTime;
                float t = disTime / burnDuration;
                
                var dissolveStrenght = Mathf.Lerp(1, 0, t);
                mat.SetFloat("_Transparency", dissolveStrenght);
            
        }
    }

    public void Ignite()
    {
        if (!isBurning)
        {
            isBurning = true;
            fireInstance.SetActive(true);
            fireInstance.transform.position = startPoint.position;
        }
    }
}
