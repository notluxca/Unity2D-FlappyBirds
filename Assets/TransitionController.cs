using UnityEngine;

public class TransitionController : MonoBehaviour
{
    [SerializeField] private Animator transitionAnimator;

    private void Start()
    {
        if (transitionAnimator == null)
        {
            transitionAnimator = GetComponent<Animator>();
        }
    }

    public void StartTransition()
    {
        transitionAnimator.Play("Transition In");
    }

    public void EndTransition()
    {
        transitionAnimator.Play("Transition Out");
    }
}
