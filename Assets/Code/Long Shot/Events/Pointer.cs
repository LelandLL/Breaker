using UnityEngine;
using System.Collections;

public class Pointer : GameEvent {

    public GameObject targetObject;
    public bool isSelected;

    public class OnSelectionChanged : Pointer
    {
        public OnSelectionChanged(GameObject targetObject, bool isSelected)
        {
            this.targetObject = targetObject;
            this.isSelected = isSelected;
        }
    }


    public class OnPointerFinished : Pointer
    {
    }

}

