using Utility;

namespace UI
{
    /// <summary>
    /// Control which shows the board for one player.
    /// </summary>
    public partial class Control_Board : UserControl
    {
        #region Properties

        /// <summary>
        /// Gets or sets the board size.
        /// </summary>
        public int BoardSize
        {
            get { return m_boardSize; }
            set { if (m_boardSize != value) { m_boardSize = value; Invalidate(); } }
        }

        /// <summary>
        /// Gets or sets the color of the grid.
        /// </summary>
        public Color GridColor
        {
            get { return m_gridColor; }
            set { if (m_gridColor != value) { m_gridColor = value; Invalidate(); } }
         }

        /// <summary>
        /// Gets or sets the border color;
        /// </summary>
        public Color BorderColor
        {
            get { return m_borderColor; }
            set { if (m_borderColor != value) { m_borderColor = value; Invalidate(); } }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public Control_Board()
        {
            InitializeComponent();
        }

        #endregion

        #region Control events

        /// <summary>
        /// Paints the control / board.
        /// </summary>
        private void Control_Board_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                // We clear the board...
                e.Graphics.Clear(Color.White);

                // We draw the grid...
                drawGrid(e.Graphics);
            }
            catch (Exception ex)
            {
                Logger.log(ex);
            }
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Dreaws the board grid.
        /// </summary>
        private void drawGrid(Graphics graphics)
        {
            // We work out the spacing of grid lines to fit the size of the control...
            var fBoardSize = (float)m_boardSize;
            var gridSizeX = Width / fBoardSize;
            var gridSizeY = Height / fBoardSize;
            var gridPen = new Pen(m_gridColor);

            // We draw the grid...
            for (var i = 1; i < m_boardSize; ++i)
            {
                // Horizontal line...
                var yOffset = i * gridSizeY;
                graphics.DrawLine(gridPen, 0, yOffset, Width - 1, yOffset);

                // Vertical line...
                var xOffset = i * gridSizeX;
                graphics.DrawLine(gridPen, xOffset, 0, xOffset, Height - 1);
            }

            // We draw the border...
            var borderPen = new Pen(m_borderColor);
            graphics.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);
        }

        #endregion

        #region Private data

        // The board size. Boards are square and have board-size squares in the x and y axes...
        private int m_boardSize = 30;

        // The color of the grid...
        private Color m_gridColor = Color.LightGray;

        // The color of the border...
        private Color m_borderColor = Color.Blue;


        #endregion
    }
}
