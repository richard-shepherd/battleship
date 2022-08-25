namespace UI
{
    public partial class Control_Board : UserControl
    {
        public Control_Board()
        {
            InitializeComponent();
        }

        private void Control_Board_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            var boardSize = 30f;
            var gridSizeX = Width / boardSize;
            var gridSizeY = Height / boardSize;
            var pen = new Pen(Color.LightGray);
            for(var i = 0; i < boardSize; ++i)
            {
                // Horizontal line...
                var yOffset = i * gridSizeY;
                e.Graphics.DrawLine(pen, 0, yOffset, Width, yOffset);

                // Vertical line...
                var xOffset = i * gridSizeX;
                e.Graphics.DrawLine(pen, xOffset, 0, xOffset, Height);
            }

            // Bottom line...
            e.Graphics.DrawLine(pen, 0, Height - 1, Width, Height - 1);

            // Right-hand line...
            e.Graphics.DrawLine(pen, Width - 1, 0, Width - 1, Height);
        }
    }
}
