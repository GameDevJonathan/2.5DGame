using UnityEngine;

public class MovingPlatForm : MonoBehaviour
{
    [SerializeField]
    private Transform StartPoint, EndPoint, TargetPoint;
    
    [SerializeField]
    private float MoveSpeed = .3f;
    
    [SerializeField]
    private bool _right = true;
    

    // Start is called before the first frame update
    void Start()
    {
        //initialize our target point to our starting point.
        TargetPoint = StartPoint;

    }
    
    //use this update method for better physic's movement
    private void FixedUpdate() 
    {        
        Moving();
    }

    private void Moving()
    {
        switch (_right) //flag to see which direction we are going in.
        {
            case true:
                //check to see if the absolute distance between these two points are bigger then a very small number
                if (Mathf.Abs(transform.position.x - TargetPoint.position.x) > Mathf.Epsilon) 
                {
                    //check to see if our x is less then target point x if we are moving right;
                    if (transform.position.x < TargetPoint.position.x)
                    {
                        transform.Translate(new Vector2(MoveSpeed * Time.deltaTime, 0));
                    }
                    else
                    {
                        //if we reached our destination change the target point and direction flag
                        TargetPoint = EndPoint;
                        _right = !_right;
                    }
                }
                break;

            case false: // see above and do in reverse
                if (Mathf.Abs(transform.position.x - TargetPoint.position.x) > Mathf.Epsilon)
                {
                    //check to see if our x is less then right point x
                    if (transform.position.x > TargetPoint.position.x)
                    {
                        transform.Translate(new Vector2(-MoveSpeed * Time.deltaTime, 0));
                    }
                    else
                    {
                        TargetPoint = StartPoint;
                        _right = !_right;
                    }
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("player landed");
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("player landed");
            other.transform.parent = null;
        }
    }

}
