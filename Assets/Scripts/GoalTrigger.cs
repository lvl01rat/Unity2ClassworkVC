using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    bool _reached = false;


    private void OnTriggerEnter(Collider other)
    {
       if (_reached) return;

       if (GameManager.Instance?.collectedCount != GameManager.Instance?.totalPickups) return;
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Goal reached!"); 
            GameManager.Instance?.OnGoalReached();
        }
    }
}
