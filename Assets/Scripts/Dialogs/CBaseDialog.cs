using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class CBaseDialog : MonoBehaviour
{
    [SerializeField] protected Animator _animator;

    protected object data;
    protected UnityAction callbackShow;

    protected const string DIALOG_SHOW_ANIM = "dialog_show";

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        this._animator = this.GetComponent<Animator>();
    }
#endif

    protected void OnEnable()
    {
        this._animator.Play(DIALOG_SHOW_ANIM);
    }

    public virtual void ClickCloseDialog()
    {
        this.OnHide();
    }

    public virtual void OnShow(object data = null, UnityAction callback = null)
    {
        this.gameObject.SetActive(true);
        this.data = data;
        this.callbackShow = callback;
    }

    public virtual void OnCompleteShow()
    {
        if (this.callbackShow != null)
        {
            var bk = this.callbackShow;
            this.callbackShow = null;
            bk.Invoke();
        }

        // Debug.Log(this.GetType().Name + " Complete Show");
    }

    public virtual void OnHide()
    {
        // this._animator.Play(ANIMATOR_HIDE);
    }

    public virtual void OnCompleteHide()
    {
        this.gameObject.SetActive(false);
    }
}