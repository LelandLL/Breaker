﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SelectableManager : MHBaseClass {


    LinkedList<GameObject> selectedObjects = new LinkedList<GameObject>();
    int targetType = -1;

    LineRenderer lineRenderer = null;
    RectTransform canvasSize = null;

    void Start()
    {
        eventBus.AddListener<Pointer.OnSelectionChanged>(OnSelectionChanged);

        lineRenderer = gameObject.GetComponent<LineRenderer>();

        canvasSize = GameObject.Find("Canvas").GetComponent<RectTransform>();
    }

    void OnDestroy()
    {
        eventBus.RemoveListener<Pointer.OnSelectionChanged>(OnSelectionChanged);
    }

    void OnSelectionChanged(Pointer.OnSelectionChanged eventData)
    {
        // find selected color
        if (eventData.isSelected)
        {
            if (targetType == -1)
            {
                targetType = eventData.targetObject.GetComponent<Selectable>().type;
            }
        }
        else
        {
            targetType = -1;
        }

        // check if tile has valid selection (wasn't already selected, is not being animated, has the same type as the others selected before, and is adjacent to the last selected)
        if (eventData.targetObject)
        {
            Selectable selectable = eventData.targetObject.GetComponent<Selectable>();
            if (eventData.isSelected)
            {
                if (IsSelectionValid(eventData.targetObject))
                {
                    selectable.isSelected = eventData.isSelected;
                    GameObject highlight = eventData.targetObject.transform.FindChild("Selection").gameObject;
                    highlight.SetActive(selectable.isSelected);

                    selectedObjects.AddLast(eventData.targetObject);

                    lineRenderer.SetVertexCount(selectedObjects.Count);
                    lineRenderer.SetPosition(selectedObjects.Count - 1, selectedObjects.Last.Value.GetComponent<RectTransform>().localPosition - new Vector3(0, canvasSize.sizeDelta.y / 2, 0));
                }
            }
        }
        else
        {
            // deselect all tiles and check which need to be destroyed
            foreach (GameObject selectedObject in selectedObjects)
            {
                Selectable selectable = selectedObject.GetComponent<Selectable>();
                if (selectable.isSelected)
                {
                    selectable.isSelected = eventData.isSelected;
                    GameObject highlight = selectedObject.transform.FindChild("Selection").gameObject;
                    highlight.SetActive(selectable.isSelected);

                    if (CanDestroy())
                    {
                        selectable.MarkTileToDestroy();
                    }
                }
            }

            if (CanDestroy())
            {
                eventBus.Publish(new Pointer.OnPointerFinished());
                eventBus.Publish(new Score.OnScoring(selectedObjects.Count));
            }

            selectedObjects.Clear();

            lineRenderer.SetVertexCount(0);
        }
    }


    bool AreTilesAdjacent(Selectable tile1, Selectable tile2)
    {
        return (tile1.row == tile2.row || tile1.row == tile2.row - 1 || tile1.row == tile2.row + 1) && (tile1.column == tile2.column || tile1.column == tile2.column - 1 || tile1.column == tile2.column + 1);
    }

    bool IsSelectionValid(GameObject gameObject)
    {
        Selectable currSelectable = gameObject.GetComponent<Selectable>();

        if (!currSelectable.isSelected && !currSelectable.needsToBeMoved && currSelectable.type == targetType)
        {
            bool belongsToGroup = false;
            if (selectedObjects.Count < 1)
            {
                belongsToGroup = true;
            }
            else
            {
                GameObject previousSelectedObject = selectedObjects.Last.Value;
                Selectable selectable = previousSelectedObject.GetComponent<Selectable>();
                if (AreTilesAdjacent(currSelectable, selectable))
                {
                    belongsToGroup = true;
                }
            }

            return belongsToGroup;
        }

        return false;
    }

    bool CanDestroy()
    {
        return selectedObjects.Count > 2;
    }
}

