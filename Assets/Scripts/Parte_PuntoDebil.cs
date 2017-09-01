using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parte_PuntoDebil : Parte_Destructible
{

    public override void BuscarUnidadEnJerarquia()
    {
        base.BuscarUnidadEnJerarquia();
        unidad.AgregarPuntoDebil(this);
    }

    public override void ImpactoLetal()
    {
        base.ImpactoLetal();
        unidad.RecibirDaño();
    }

    private void OnDestroy()
    {
        unidad.QuitarPuntoDebil(this);
    }
}
