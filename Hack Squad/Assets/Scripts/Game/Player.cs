using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase base de los jugadores presentes en el juego
/// </summary>
public class Player : MonoBehaviour {

    int idPlayer;

    string nameOfPlayer;

    public enumPlayerTypes playerType;

    //public bool isMoving;

    //public Vector3 targetPosition;

    public Board board;

    bool executeUpdate;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Variables de tipo enum que simulan los dos tipos de personajes presentes en el juego
    /// </summary>
    public enum enumPlayerTypes
    {
        Virus, Antivirus
    }


    /// <summary>
    /// Método que ejecuta la programación dada por el usuario, dependiendo del tipo de jugador
    /// </summary>
    public virtual void ExecuteProgramming()
    {

    }

    /// <summary>
    /// Método que ubica al player en alguno de los posiciones de carril en un determinado carril
    /// </summary>
    /// <param name="rail">Parametro de entrada que simula alguno de los 4 carriles de juego</param>
    /// <param name="positionOfVector">Parametro de entrada que simula una de las posiciones en un carril de juego</param>
    /// <returns>Las coordenadas x, y de la posición del carril selecionado</returns>
    public Vector2 TakeRail(int rail, int positionOfVector)
    {
        Vector2 positionRail = new Vector2(0, 0);
        switch (rail)
        {
            case 1:
                positionRail = new Vector2(board.rail1.listOfPointsRail[positionOfVector].transform.position.x, board.rail1.listOfPointsRail[positionOfVector].transform.position.y);
                break;
            case 2:
                positionRail = new Vector2(board.rail2.listOfPointsRail[positionOfVector].transform.position.x, board.rail2.listOfPointsRail[positionOfVector].transform.position.y);
                break;
            case 3:
                positionRail = new Vector2(board.rail3.listOfPointsRail[positionOfVector].transform.position.x, board.rail3.listOfPointsRail[positionOfVector].transform.position.y);
                break;
            case 4:
                positionRail = new Vector2(board.rail4.listOfPointsRail[positionOfVector].transform.position.x, board.rail4.listOfPointsRail[positionOfVector].transform.position.y);
                break;
        }
        return positionRail;
    }




}
