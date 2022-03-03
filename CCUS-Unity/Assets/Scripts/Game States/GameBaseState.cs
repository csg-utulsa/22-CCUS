/**********
 * Created by: Coleton Wheeler
 * Created on: 3/1/22
 * 
 * Last edited by: N/A
 * Last edited on: N/A
 * 
 * Description: Abstract class for Game States to derive from.
 *****/


using UnityEngine;

public abstract class GameBaseState
{
    public abstract void EnterState();

    public abstract void UpdateState();
}
