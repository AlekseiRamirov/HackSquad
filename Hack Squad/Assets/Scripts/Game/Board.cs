using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que contiene a las cartas y el vector de los puntos de juego,
/// Aqui se encuentran los métodos para barajar las cartas y moverlas a su posición correspondiente
/// </summary>
public class Board : MonoBehaviour
{

    /// <summary>
    /// Vector que contiene las 40 cartas presentes en el juego
    /// </summary>
    public GameObject[] listOfCards;

    /// <summary>
    /// Existen 4 vectores en el juego, que poseen cada uno de a 10 posiciones, por donde se mueven los personajes
    /// </summary>
    public Rail rail1;
    public Rail rail2;
    public Rail rail3;
    public Rail rail4;

    public GameObject prefabCardString;
    public GameObject prefabCardBool;
    public GameObject prefabCardDouble;
    public GameObject prefabCardInteger;

    bool mixCards = false;
    bool generateCards = false;
    bool moveCards = false;

    

    void Start()
    {
        listOfCards = new GameObject[40];
    }

    
    void Update()
    {
        if (mixCards && generateCards && moveCards)
        {
            MoveCardsToPosition();
        }
    }

    /// <summary>
    /// Método utilizado para barajar las 40 cartas en un solo gran vector,
    /// para posteriormente ubicarlas en las posiciones de los 4 vectores del juego
    /// </summary>
    public void MixCards()
    {
        var quantityOfCards = listOfCards.Length; // Se toma la cantidad de cartas del vector en una variable.
        System.Random random = new System.Random();
        for (int i = 0; i < listOfCards.Length; i++)
        {
            var j = random.Next(0, i); // Se lanza un ramdon entre los valores 0 y el numero que tenga la variable i
            var temp = listOfCards[i]; // Se guarda en una variable temporal la carta en la posición i
            listOfCards[i] = listOfCards[j]; // En la posición i se le asigna la carta que contiene la posicion j generada
            listOfCards[j] = temp; // Y en la posición j generada se le asigna lo que estaba guardado en la variable temp
        }
        mixCards = true;
        Debug.Log(mixCards);
    }

    /// <summary>
    /// Método que mueve todas las cartas a su posicion determinada en cada uno de los 4 vectores del juego
    /// </summary>
    public void MoveCardsToPosition()
    {
        int cont = 0;
        for (int i = 0; i < 10; i++)
        {
            //listOfCards[cont].transform.position = Vector2.MoveTowards(listOfCards[i].transform.position, rail1.listOfPointsRail[i].transform.position, Time.deltaTime * 1.5f);
            //cont++;
            //listOfCards[cont].transform.position = Vector2.MoveTowards(listOfCards[i].transform.position, rail2.listOfPointsRail[i].transform.position, Time.deltaTime * 1.5f);
            //cont++;
            //listOfCards[cont].transform.position = Vector2.MoveTowards(listOfCards[i].transform.position, rail3.listOfPointsRail[i].transform.position, Time.deltaTime * 1.5f);
            //cont++;
            //listOfCards[cont].transform.position = Vector2.MoveTowards(listOfCards[i].transform.position, rail4.listOfPointsRail[i].transform.position, Time.deltaTime * 1.5f);
            //cont++;

            //listOfCards[cont].transform.Translate(rail1.listOfPointsRail[i].transform.position.x * Time.deltaTime, rail1.listOfPointsRail[i].transform.position.y * Time.deltaTime, 0);
            //cont++;
            //listOfCards[cont].transform.Translate(rail2.listOfPointsRail[i].transform.position.x * Time.deltaTime, rail2.listOfPointsRail[i].transform.position.y * Time.deltaTime, 0);
            //cont++;
            //listOfCards[cont].transform.Translate(rail3.listOfPointsRail[i].transform.position.x * Time.deltaTime, rail3.listOfPointsRail[i].transform.position.y * Time.deltaTime, 0);
            //cont++;
            //listOfCards[cont].transform.Translate(rail4.listOfPointsRail[i].transform.position.x * Time.deltaTime, rail4.listOfPointsRail[i].transform.position.y * Time.deltaTime, 0);
            //cont++;
            // Cada carta se ordena en forma vertical en los 4 vectores que simulan los carriles de juego
            listOfCards[cont].transform.position = new Vector3(rail1.listOfPointsRail[i].transform.position.x, rail1.listOfPointsRail[i].transform.position.y);
            cont++;
            listOfCards[cont].transform.position = new Vector3(rail2.listOfPointsRail[i].transform.position.x, rail2.listOfPointsRail[i].transform.position.y);
            cont++;
            listOfCards[cont].transform.position = new Vector3(rail3.listOfPointsRail[i].transform.position.x, rail3.listOfPointsRail[i].transform.position.y);
            cont++;
            listOfCards[cont].transform.position = new Vector3(rail4.listOfPointsRail[i].transform.position.x, rail4.listOfPointsRail[i].transform.position.y);
            cont++;

        }
        moveCards = false;
    }

    /// <summary>
    /// Método que crea las 40 cartas en el tablero de juego en una posición dada
    /// </summary>
    public void GenerateCards()
    {
        Vector3 generatorPosition = new Vector3(transform.position.x, transform.position.y, 0);      
        for (int i = 1; i <= 40; i++)
        {          
            if (i > 0 && i <= 10)
            {
                prefabCardString.GetComponent<Card>().typeCard = Card.enumTypesCards.String;
                listOfCards[i-1] = Instantiate(prefabCardString, generatorPosition, Quaternion.identity);               
            }
            else if (i > 10 && i <= 20)
            {
                prefabCardBool.GetComponent<Card>().typeCard = Card.enumTypesCards.Bool;
                listOfCards[i-1] = Instantiate(prefabCardBool, generatorPosition, Quaternion.identity);                
            }
            else if (i > 20 && i <= 30)
            {
                prefabCardDouble.GetComponent<Card>().typeCard = Card.enumTypesCards.Double;
                listOfCards[i-1] = Instantiate(prefabCardDouble, generatorPosition, Quaternion.identity);                
            }
            else
            {
                prefabCardInteger.GetComponent<Card>().typeCard = Card.enumTypesCards.Integer;
                listOfCards[i-1] = Instantiate(prefabCardInteger, generatorPosition, Quaternion.identity);              
            }                               
        }
        generateCards = true;
        Debug.Log(generateCards);
    }

    public void SetBoolMoveCards()
    {
        moveCards = true;
        Debug.Log(moveCards);
    }

}
