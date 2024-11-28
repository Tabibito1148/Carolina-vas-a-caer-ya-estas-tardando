using System.Collections;
using UnityEngine;

public class MovimientoStates : MonoBehaviour
{
    public Joystick joystick;

    public Animator dongojo;
    public Rigidbody2D rigid;

    Basestate estadoActual;

    public EstadoIdle idle;
    public EstadoCorrer correr;
    public EstadoSalto salto;
    public EstadoCaida caida;
    public EstadoSaltoDoble saltoDoble;
    public EstadoSaltoPared saltoPared;

    public float fuerzaSalto;
    public float horizontal;
    public int velocidad;

    [SerializeField] Transform centroDeteccion;
    [SerializeField] LayerMask capasDeteccion;
    [SerializeField] public KeyCode teclas;
    [SerializeField] Vector2 tamañoDeteccion;
    public bool tocandoPiso;
    public bool saltoActivado;

    void Start()
    {
        dongojo = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        idle = new EstadoIdle(this); //"this" pasará toda la instancia en la que se encuentre 
        correr = new EstadoCorrer(this);
        salto = new EstadoSalto(this);
        caida = new EstadoCaida(this);
        saltoDoble = new EstadoSaltoDoble(this);
        saltoPared = new EstadoSaltoPared(this);

        CambiarEstado(idle);
    }
    void Update()
    {
       
        horizontal = joystick.Horizontal;
        estadoActual.StateUpdate();

        Flip();
    }

    private void FixedUpdate()
    {
        estadoActual.FixedUpdateState();

        tocandoPiso = Physics2D.OverlapBox(centroDeteccion.position, tamañoDeteccion, 0, capasDeteccion);
    }

    public void CambiarEstado(Basestate nuevoEstado)
    {
        estadoActual = nuevoEstado;
        estadoActual.StateStart();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(centroDeteccion.position, tamañoDeteccion);
        Gizmos.color = Color.red;
    }

    private void Flip()
    {
        if (horizontal < 0 && transform.localEulerAngles.y == 0 || horizontal > 0 && transform.localEulerAngles.y == 180)
        {
            transform.localEulerAngles = new Vector3(transform.eulerAngles.x, horizontal > 0 ? 0 : 180, transform.eulerAngles.z);
        }
    } 
    public void EjecutarSalto()
    {
        StartCoroutine(EjecutarSaltoCorrutina());
    }
    IEnumerator EjecutarSaltoCorrutina() 
    {
        saltoActivado = true;
        yield return new WaitForEndOfFrame();
        saltoActivado=false;
        
    }
}
