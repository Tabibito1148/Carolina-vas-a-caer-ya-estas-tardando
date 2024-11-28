using UnityEngine;

public class EstadoIdle : Basestate
{
    public EstadoIdle(MovimientoStates controladorParametros) : base(controladorParametros) //Base se refiere a la clase padre
    {

    }
    public override void StateStart()
    {
        controlador.dongojo.Play("Idle");
        controlador.rigid.linearVelocity = new Vector2(0, controlador.rigid.linearVelocity.y);
    }
    public override void StateUpdate()
    {
        if (controlador.rigid.linearVelocity.y <= 0) 
        {
            if (controlador.tocandoPiso)
            {
                if (Input.GetKeyDown(controlador.teclas) || controlador.saltoActivado) 
                {
                    StateExit(controlador.salto);
                }
                else if (controlador.horizontal != 0)
                {
                    StateExit(controlador.correr);
                }
            }

            else 
            {
                StateExit(controlador.caida);
            }
        }
    }
    public override void FixedUpdateState()
    {
        
    }
    public override void StateExit(Basestate newState)
    {
        controlador.CambiarEstado(newState);
    }

}
