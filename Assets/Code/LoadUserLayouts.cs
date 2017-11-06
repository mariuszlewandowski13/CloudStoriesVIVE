using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUserLayouts : MonoBehaviour {

    public GameObject buttonPrefab;

    public GameObject scroll;

    public float yStartPosition = 0.36f;



    private void OnEnable()
    {
        LoadLayouts();
    }

    public void LoadLayouts()
    {
        if (ApplicationStaticData.layoutsNumber == null)
        {
            ApplicationStaticData.layoutsNumber = new List<int>();
            GameObject.Find("LoadScene").GetComponent<DatabaseController>().LoadUserLayouts(ShowUserLayouts);
        }
        else {
            ShowUserLayouts();
        }
    }

    public void ShowUserLayouts()
    {
        ClearButtons();
        float actualYPosition = yStartPosition;
        foreach (int layNumb in ApplicationStaticData.layoutsNumber)
        {
            GameObject newButton = Instantiate(buttonPrefab);
            newButton.transform.parent = scroll.transform;

            newButton.transform.position = scroll.transform.position;
            newButton.transform.localPosition += new Vector3(0.5f, actualYPosition, 0.0f);

            actualYPosition -= 0.22f;

           newButton.transform.Find("Text").GetComponent<TextMesh>().text = layNumb.ToString();


            newButton.GetComponent<LaytouChangeButton>().layoutNumber = layNumb;
            
        }
    }

    public void ClearButtons()
    {
        if (scroll != null)
        {
            foreach (Transform child in scroll.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }


}
