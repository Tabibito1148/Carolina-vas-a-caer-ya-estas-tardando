using UnityEngine;

public class controladorEnemigos : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rg;
    public Collider2D objetivo;
    public float velocidad;
    public Transform centroVision;
    public Vector2 tama�oVision;
    public LayerMask capaVision;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        rg = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        objetivo = Physics2D.OverlapBox(centroVision.position, tama�oVision, 0, capaVision);
        anim.SetBool("jugadorDetectado", objetivo);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(centroVision.position, tama�oVision);
    }
}
