using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Ground : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public float speed;

    private void Awake()
    {
        //meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (GameManager.Instance.enabled && !GameManager.Instance.isPaused)
        {
            //float speed = 0.05f; //GameManager.Instance.gameSpeed / transform.localScale.x;
            meshRenderer.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
        }    
    }

}
