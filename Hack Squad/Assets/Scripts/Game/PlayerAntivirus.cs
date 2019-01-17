using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que contiene los atributos y los métodos del jugador Antivirus
/// </summary>
public class PlayerAntivirus : Player
{

    public int conditionalPosition;

    public bool axisConditionX;

    [Range(1, 4, order = 0)]
    public int initialRail;

    [Range(0, 9, order = 0)]
    public int initialPositionInRail;

    public enumDirections[] arrayDirections;


    // Use this for initialization
    void Start()
    {
        playerType = enumPlayerTypes.Antivirus;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Variables de tipo enum, que definen los movimientos del personaje Antivirus
    /// </summary>
    public enum enumDirections
    {
        Derecha, Izquierda, Arriba, Abajo
    }

    /// <summary>
    /// Método que sobreescribe al de la clase base "Player"
    /// </summary>
    public override void ExecuteProgramming()
    {
        base.ExecuteProgramming();
        StartCoroutine(MoveToDirections());
    }

    /// <summary>
    /// Método que ubica inicialmente al player antivirus
    /// </summary>
    public void SetInitialPosition()
    {
        transform.position = base.TakeRail(initialRail, initialPositionInRail);
    }

    /// <summary>
    /// Método que mueve al personaje Antivirus de acuerdo a las direcciones asignadas por el Player
    /// </summary>
    /// <returns>Retorna un movimiento cada 1.5 segundos</returns>
    public IEnumerator MoveToDirections()
    {
        for (int i = 0; i < arrayDirections.Length; i++)
        {
            if (arrayDirections[i].Equals(enumDirections.Derecha))
            {              
                transform.position = base.TakeRail(initialRail, initialPositionInRail + 1);
                initialPositionInRail++;
                CheckConditions();
            }
            else if (arrayDirections[i].Equals(enumDirections.Izquierda))
            {               
                transform.position = base.TakeRail(initialRail, initialPositionInRail - 1);
                initialPositionInRail--;
                CheckConditions();
            }
            else if (arrayDirections[i].Equals(enumDirections.Abajo))
            {               
                transform.position = base.TakeRail(initialRail + 1, initialPositionInRail);
                initialRail++;
                CheckConditions();
            }
            else if (arrayDirections[i].Equals(enumDirections.Arriba))
            {
                transform.position = base.TakeRail(initialRail - 1, initialPositionInRail);
                initialRail--;
                CheckConditions();
            }
            yield return new WaitForSecondsRealtime(1.5f);
        }
    }

    public void CheckConditions()
    {
        
        if (transform.position == board.rail1.listOfPointsRail[conditionalPosition].transform.position) 
        {
            StopAllCoroutines();
        }
        //else if (transform.position == board.rail2.listOfPointsRail[conditionalPosition].transform.position)
        //{
        //    StopAllCoroutines();
        //}
        //else if (transform.position == board.rail3.listOfPointsRail[conditionalPosition].transform.position)
        //{
        //    StopAllCoroutines();
        //}
        //else if (transform.position == board.rail4.listOfPointsRail[conditionalPosition].transform.position)
        //{
        //    StopAllCoroutines();
        //}
    }

}
