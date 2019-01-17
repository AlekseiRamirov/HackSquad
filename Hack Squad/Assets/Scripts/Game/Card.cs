using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que contiene los atributos y métodos de cada carta
/// </summary>
public class Card : MonoBehaviour {

    /// <summary>
    /// Enumeradores que definen los 4 tipos de cartas presentes en el juego
    /// </summary>
    public enum enumTypesCards
    {
        String, Bool, Double, Integer
    }

    /// <summary>
    /// Variable de tipo "enumTypesCards" que almacena el tipo de carta correspondiente
    /// </summary>
    public enumTypesCards typeCard;

    /// <summary>
    /// Método que obtiene el tipo de carta correspondiente a cada una
    /// </summary>
    public enumTypesCards GetCardsTypes()
    {
        return typeCard;
    }
       
}

