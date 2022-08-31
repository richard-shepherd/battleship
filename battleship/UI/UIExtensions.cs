namespace UI
{
    /// <summary>
    /// Extensions for UI elements.
    /// </summary>
    internal static class UIExtensions
    {
        /// <summary>
        /// Checks or unchecks all items in a CheckedListBox.
        /// </summary>
        public static void CheckAll(this CheckedListBox checkedListBox, bool check)
        {
            for (int i = 0; i < checkedListBox.Items.Count; ++i)
            {
                checkedListBox.SetItemChecked(i, check);
            }
        }
    }
}
