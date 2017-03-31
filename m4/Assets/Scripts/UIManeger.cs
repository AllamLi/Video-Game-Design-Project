using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManeger : MonoBehaviour
{
    public Animator initOpenAnimator;

    private int m_OpenParaId;
    private Animator m_Open;
    private GameObject m_PrevSelected;

    const string k_OpenTransitionName = "Open";
    const string K_ClosedStateName = "Closed";

    public void OnEnable()
    {
        m_OpenParaId = Animator.StringToHash(k_OpenTransitionName);
        if (initOpenAnimator == null)
            return;
        OpenPanel(initOpenAnimator);
    }

    public void OpenPanel(Animator animator)
    {
        if (m_Open == animator) return;
        animator.gameObject.SetActive(true);
        GameObject newPrevSelected = null;
        if (EventSystem.current != null)
            newPrevSelected = EventSystem.current.currentSelectedGameObject;

        animator.transform.SetAsLastSibling();
        CloseCurrent();

        m_PrevSelected = newPrevSelected;

        m_Open = animator;
        m_Open.SetBool(m_OpenParaId, true);
        GameObject go = FindFirstSelectable(animator.gameObject);

        SetSelected(go);
    }

    static GameObject FindFirstSelectable(GameObject obj)
    {
        GameObject go = null;
        var selectables = obj.GetComponentsInChildren<Selectable>(true);
        foreach(var selectable in selectables)
        {
            if (selectable.IsActive() && selectable.IsInteractable())
            {
                go = selectable.gameObject;
                break;
            }
        }
        return go;
    }

    public void CloseCurrent()
    {
        if (m_Open == null)
            return;

        m_Open.SetBool(m_OpenParaId, false);
        SetSelected(m_PrevSelected);
        StartCoroutine(DisablePanelDelayed(m_Open));
        m_Open = null;
    }

    private void SetSelected(GameObject obj)
    {
        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(obj);
    }

    IEnumerator DisablePanelDelayed(Animator animator)
    {
        bool closedStatedReached = false;
        bool wantToClose = true;

        while (!closedStatedReached && wantToClose)
        {
            if (!animator.IsInTransition(0))
                closedStatedReached = animator.GetCurrentAnimatorStateInfo(0).IsName(K_ClosedStateName);

            wantToClose = !animator.GetBool(m_OpenParaId);

            yield return new WaitForEndOfFrame();
        }

        if (wantToClose)
            animator.gameObject.SetActive(false);
    }
}
