using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    //Necesito para este Manager, un builder para los elementos, otro para el framework, un controlador de botones y 
    //un gestor de Updates.
    //El menu funcionaria de la siguiente manera, primero, se contruyen los elementos del menu, luego se llama al 
    //builder del framework, si es que lo tiene claro, una vez el framework este adaptado a los elementos del menu
    //la parte del UI ya estaria terminara, eso si, el controlador de Updates debe tener acceso a ciertos elementos 
    //del menu, por si tiene actualizar las bases de datos o recrear los elementos del menu
    
    /*Para cumplir esto pues necesito que cada manager tenga una interfaz padre
    El builder, aparte de connstruir los elementos, deberia devolver o almacenar las referencias de los gameobjects,
    el vector con las medidas de cada elemento.
    El framework es un elemento opcional, por lo que debe ser construido en el builder de elementos quizas????
    pues este se encargaria de construir el framework y ajustar los elementos dentro de el
    Eso si esto no funcionaria sin una base de datos que se encargue de crearlo eso si ni puta idea de donde ponerla
    aunque probablemente se encuentre en el manager principal ya que el Update debe tener acceso a ella, para actualizar
    la informacion
    
    
    
    */
}
