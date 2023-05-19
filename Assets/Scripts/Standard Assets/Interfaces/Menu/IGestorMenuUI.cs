
public interface IGestorMenuUI
{
    public void StartToControlMenu(IPlainOptions menuThatIWillManage, bool doYouNeedToStopTwoCall);
    void ResetValuesBeforeMenu();
    public void MovePointerToAnotherOption(int addAndSubtract);
    public void SelectOption();
}
