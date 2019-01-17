using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que contiene los atributos y los método de movimiento del jugador Virus
/// </summary>
public class PlayerVirus : Player {

    
    [Range(0, 9, order = 0)]
    public int initialPosition;

    [Range(0, 9, order = 0)]
    public int conditionalPosition;

    [Range(0, 9, order = 0)]
    public int incrementalPosition;
    
    [Range(1,4,order = 0)]
    public int railSelected;
    
    void Start()
    {
        playerType = enumPlayerTypes.Virus;
    }

    /// <summary>
    /// Método que sobreescribe al de la clase base "Player"
    /// </summary>
    public override void ExecuteProgramming()
    {
        base.ExecuteProgramming();
        StartCoroutine(MoveToConditionalPosition());     
    }


    /// <summary>
    /// Método que mueve al personaje Virus según la programación dada por el Player
    /// </summary>
    /// <returns>Retorna un movimiento cada 1.5 segundos</returns>
    public IEnumerator MoveToConditionalPosition()
    {
        if (conditionalPosition > initialPosition)
        {
            for (int i = initialPosition; i < conditionalPosition + 1; i += incrementalPosition)
            {
                transform.position = base.TakeRail(railSelected, i);
                yield return new WaitForSecondsRealtime(1.5f);
            }
        }
        else
        {
            for (int i = initialPosition; i > conditionalPosition - 1; i -= incrementalPosition)
            {
                transform.position = base.TakeRail(railSelected, i);
                yield return new WaitForSecondsRealtime(1.5f);
            }
        }
        
    }

    
}
