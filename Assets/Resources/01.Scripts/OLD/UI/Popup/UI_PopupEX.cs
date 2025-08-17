public class UI_PopupEX : UI_BaseEX
{
    public override void Init()
    {
        GameManager.Ui.SetCanvase(gameObject, true);
    }

    public virtual void ClosePopupUI()
    {
        GameManager.Ui.ClosePopupUI(this);
    }

}
