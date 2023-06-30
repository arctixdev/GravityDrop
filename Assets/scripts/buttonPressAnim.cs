using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class buttonPressAnim : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Button button;

    [SerializeField]
    private bool disableButtonAutoAnimator;



    public void playAnim(string name)
    {
        if(disableButtonAutoAnimator){
            button.transition = Selectable.Transition.None;
            StartCoroutine(startAnimDelayed(name));
        }
        else {
            animator.Play(name);
        }

    }
    public IEnumerator startAnimDelayed(string name)
    {
        yield return null;
        animator.Play(name);

    }


    
    
}
