using Unity.VisualScripting;
using UnityEngine;

public class KeepPagesAttached 
{
    private GameObject newPage;
    private GameObject oldRightPage;
    private GameObject oldLeftPage;
    private Animator oldPageAnimator;

    private RectTransform leftTransform;
    private RectTransform rightTransform;

    public KeepPagesAttached(GameObject newPage, GameObject oldRightPage, GameObject oldLeftPage)
    {
        this.newPage = newPage;
        this.oldRightPage = oldRightPage;
        this.oldLeftPage = oldLeftPage;

        leftTransform = newPage.GetComponent<RectTransform>();
        rightTransform = oldRightPage.GetComponent<RectTransform>();

        oldPageAnimator = oldRightPage.GetComponent<Animator>();
        oldPageAnimator.SetTrigger("Flip");
    }

    public bool Turn()  
    {
/*        if (oldPageAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {*/
            newPage.transform.localRotation = Quaternion.Euler(oldRightPage.transform.localRotation.eulerAngles + new Vector3(180, -90, -180));
            Debug.Log("is playing");
            return true;
/*        }
        else
        {
            //Destroy(oldRightPage);
            //Destroy(oldLeftPage);
            Debug.Log("finished playing");
            return false;
        }*/
    }
}
