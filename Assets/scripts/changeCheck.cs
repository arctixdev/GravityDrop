using UnityEngine;
public class changeCheck : MonoBehaviour
{
    private object Var;
    public object oldVar;

    public bool check(object newVar){
        oldVar = Var;
        if(Var != newVar){
            Var = newVar;
            return true;
        }
        return false;
    }
}