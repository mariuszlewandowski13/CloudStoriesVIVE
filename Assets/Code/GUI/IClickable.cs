using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable  {

    void Clicked(Vector3 clickPosition, GameObject clickingObject);
}
