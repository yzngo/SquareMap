namespace JoyNow.SLG
{
    [System.Flags]
    public enum CellStates
    {
        None            =   0,
        Interactable    =   1 << 0,
    }
    
    public static class CellStatesExtension
    {
        public static bool HasState(this CellStates states, CellStates state)
        {
            return (states & state) != 0;
        }
        
        public static void AddState(this ref CellStates states, CellStates state)
        {
            states |= state;
        }
        
        public static void RemoveState(this ref CellStates states, CellStates state)
        {
            states &= ~state;
        }
    }
}