using System.Collections.Generic;
using UnityEngine;

public class ScaleHandlerScript : MonoBehaviour {

    #region Private Properties

    public GameObject parentObject
    {
        get {
            return gameObject.transform.parent.gameObject;
        }
    }

    private List<GameObject> resizeObjectsQueue;

    private List<Transform> children;

    public object tempObjectLock = new object();
    private object queueLock = new object();

    private GameObject actualResizeObject = null;

    #endregion

    #region Public Properties

    public Material transparentMaterial;

    #endregion

    #region Methods

    void Start()
    {
        
        resizeObjectsQueue = new List<GameObject>();
        children = new List<Transform>();

        SetChildren();
        if (parentObject.GetComponent<BoxCollider>())
        {
            UpdateChildrenPresentation(parentObject.GetComponent<BoxCollider>());

        }
        
    }

    public GameObject GetParentObject()
    {
        return parentObject;
    }

    private void SetChildren()
    {
        foreach (Transform child in gameObject.transform)
        {
            if (child.GetComponent<Renderer>() != null) children.Add(child);
        }
    }

    public GameObject AddNewResizingObject(GameObject newObject)
    {
        lock (queueLock)
        {
            resizeObjectsQueue.Add(newObject);
            if (resizeObjectsQueue.Count == 1)
            {
                GetComponent<ObjectsFadeScript>().canFade = false;
                CreateResizingImage();
                UpdateChildrenParenting(actualResizeObject.transform);

                if (parentObject.GetComponent<ImageMoveScript>() != null)
                {
                    parentObject.GetComponent<ImageMoveScript>().canMove = false;
                }
            }
        }

        return actualResizeObject;
    }
    public void RemoveResizingObject(GameObject removedObject)
    {
        lock (queueLock)
        {
            resizeObjectsQueue.Remove(removedObject);
            if (resizeObjectsQueue.Count == 0)
            {
                UpdateParentImageObject();
                GetComponent<ObjectsFadeScript>().canFade = true;
                UpdateImageObjectScale();
                UpdateChildrenParenting(transform);
                //UpdateChildrenScale(removedObject);

                if (parentObject.GetComponent<ImageMoveScript>() != null)
                {
                    parentObject.GetComponent<ImageMoveScript>().canMove = true;
                }
                Destroy(actualResizeObject);
            }
        }
    }

    private void CreateResizingImage()
    {
        
        actualResizeObject = Instantiate(parentObject, parentObject.transform.position, parentObject.transform.rotation);

        foreach (Transform child in actualResizeObject.transform)
        {
            if (child.name == "ScaleHandler")
            {
                Destroy(child.gameObject);
            }
           
        }


        if (actualResizeObject.GetComponent<Renderer>() != null && actualResizeObject.GetComponent<Renderer>().material.HasProperty("_Color"))
        {
            Color color = actualResizeObject.GetComponent<Renderer>().material.color;
            color.a = 0.5f;
            if (transparentMaterial != null)
            {
                actualResizeObject.GetComponent<Renderer>().material = transparentMaterial;
            }

            actualResizeObject.GetComponent<Renderer>().material.color = color;
        }

        actualResizeObject.transform.parent = parentObject.transform.parent;
        actualResizeObject.transform.position = parentObject.transform.position;
        actualResizeObject.transform.rotation = parentObject.transform.rotation;
        actualResizeObject.transform.localScale = parentObject.transform.localScale;

        actualResizeObject.transform.Translate(-Vector3.down * 0.005f, parentObject.transform);

        if (actualResizeObject.GetComponent<ImageMoveScript>() != null)
        {
            DestroyImmediate(actualResizeObject.GetComponent<ImageMoveScript>());
        }
        
    }

    private void UpdateParentImageObject()
    {
        parentObject.transform.localScale = actualResizeObject.transform.localScale;
        parentObject.transform.position = actualResizeObject.transform.position;
        parentObject.transform.Translate(Vector3.down * 0.005f, parentObject.transform);
    }

    public void UpdateChildrenParenting(Transform newParent)
    {
        foreach (Transform child in children)
        {
            child.parent = newParent;
        }
    }

    public void UpdateChildrenPresentation(BoxCollider parent)
    {
        foreach (Transform child in transform)
        {
            Vector3 newPosition = new Vector3();
            if (child.name == "Top1" || child.name == "Top1Scaling")
            {
                newPosition = parent.bounds.max;
                newPosition.x = parent.bounds.min.x;
            }
            else if (child.name == "Top2" || child.name == "Top2Scaling" || child.name == "TopLeft" || child.name == "TopLeftScaling") newPosition = parent.bounds.max;
            else if (child.name == "Top3" || child.name == "Top3Scaling")
            {
                newPosition = parent.bounds.max;
                newPosition.z = parent.bounds.min.z;
            }
            else if (child.name == "Top4" || child.name == "Top4Scaling" || child.name == "BottomLeft" || child.name == "BottomLeftScaling")
            {
                newPosition = parent.bounds.min;
                newPosition.y = parent.bounds.max.y;
            }
            else if (child.name == "Bottom4" || child.name == "Bottom4Scaling" || child.name == "BottomRight" || child.name == "BottomRightScaling") newPosition = parent.bounds.min;
            else if (child.name == "Bottom2" || child.name == "Bottom2Scaling" || child.name == "TopRight" || child.name == "TopRightScaling")
            {
                newPosition = parent.bounds.max;
                newPosition.y = parent.bounds.min.y;
            }
            else if (child.name == "Bottom3" || child.name == "Bottom3Scaling")
            {
                newPosition = parent.bounds.min;
                newPosition.x = parent.bounds.max.x;
            }
            else if (child.name == "Bottom1" || child.name == "Bottom1Scaling")
            {
                newPosition = parent.bounds.min;
                newPosition.z = parent.bounds.max.z;
            }
            else if (child.name == "Side1" || child.name == "Side1Scaling")
            {
                newPosition = parent.bounds.center;
                newPosition.z = parent.bounds.max.z;
            }
            else if (child.name == "Side2" || child.name == "Side2Scaling")
            {
                newPosition = parent.bounds.center;
                newPosition.z = parent.bounds.min.z;
            }
            else if (child.name == "Side3" || child.name == "Side3Scaling")
            {
                newPosition = parent.bounds.center;
                newPosition.x = parent.bounds.min.x;
            }
            else if (child.name == "Side4" || child.name == "Side4Scaling")
            {
                newPosition = parent.bounds.center;
                newPosition.x = parent.bounds.max.x;
            }
            else if (child.name == "Side5" || child.name == "Side5Scaling" || child.name == "Left" || child.name == "LeftScaling")
            {
                newPosition = parent.bounds.center;
                newPosition.y = parent.bounds.max.y;
            }
            else if (child.name == "Side6" || child.name == "Side6Scaling" || child.name == "Right" || child.name == "RightScaling")
            {
                newPosition = parent.bounds.center;
                newPosition.y = parent.bounds.min.y;
            }
            else if (child.name == "Top" || child.name == "TopScaling")
            {
                newPosition = parent.bounds.max;
                newPosition.y = parent.bounds.center.y;
            }
            else if (child.name == "Bottom" || child.name == "BottomScaling")
            {
                newPosition = parent.bounds.min;
                newPosition.y = parent.bounds.center.y;
            }







            child.transform.position = newPosition;
        }
    }

    private void UpdateImageObjectScale()
    {
        float width = parentObject.transform.localScale.x;
        float height = parentObject.transform.localScale.z;
    }

    #endregion
}
