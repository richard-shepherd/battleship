using GameEngine;
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
            set { setBoardSize(value); }
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
        /// Gets or sets the player color;
        /// </summary>
        public Color PlayerColor
        {
            get { return m_playerColor; }
            set { if (m_playerColor != value) { m_playerColor = value; Invalidate(); } }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public Control_Board()
        {
            InitializeComponent();
            updateGridSize();
        }

        /// <summary>
        /// Shows the board.
        /// </summary>
        public void showBoard(Board board)
        {
            m_board = board;
            Invalidate();
        }

        #endregion

        #region Control events

        /// <summary>
        /// Called when the control is redrawn.
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

        /// <summary>
        /// Called when the control is resized.
        /// </summary>
        private void Control_Board_Resize(object sender, EventArgs e)
        {
            try
            {
                updateGridSize();
                Invalidate();
            }
            catch (Exception ex)
            {
                Logger.log(ex);
            }
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Draws the ships and other items from the board.
        /// </summary>
        public void drawBoard()
        {
            if(m_board != null)
            {
                drawShips();
                drawMines();
                drawDrones();
            }
        }

        /// <summary>
        /// Draws each ship on the board.
        /// </summary>
        private void drawShips()
        {
            // TODO: WRITE THIS!!!
        }

        private void drawMines()
        {
            // TODO: WRITE THIS!!!
        }

        private void drawDrones()
        {
            // TODO: WRITE THIS!!!
        }

        /// <summary>
        /// Sets the board size and redraws the grid.
        /// </summary>
        private void setBoardSize(int boardSize)
        {
            m_boardSize = boardSize;
            m_fBoardSize = boardSize;
            updateGridSize();
            Invalidate();
        }

        /// <summary>
        /// Updates the size of grid squares based on the board size and the size of the control.
        /// </summary>
        private void updateGridSize()
        {
            m_gridSizeX = Width / m_fBoardSize;
            m_gridSizeY = Height / m_fBoardSize;
        }

        /// <summary>
        /// Dreaws the board grid.
        /// </summary>
        private void drawGrid(Graphics graphics)
        {
            // We work out the spacing of grid lines to fit the size of the control...
            var gridPen = new Pen(m_gridColor);

            // We draw the grid...
            for (var i = 1; i < m_boardSize; ++i)
            {
                // Horizontal line...
                var yOffset = i * m_gridSizeY;
                graphics.DrawLine(gridPen, 0, yOffset, Width - 1, yOffset);

                // Vertical line...
                var xOffset = i * m_gridSizeX;
                graphics.DrawLine(gridPen, xOffset, 0, xOffset, Height - 1);
            }

            // We draw the border...
            var borderPen = new Pen(m_playerColor);
            graphics.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);
        }

        #endregion

        #region Private data

        // The board size and associated grid size...
        private int m_boardSize = 30;
        private float m_fBoardSize = 30f;
        private float m_gridSizeX = 10f;
        private float m_gridSizeY = 10f;

        // The color of the grid...
        private Color m_gridColor = Color.LightGray;

        // The color of the player...
        private Color m_playerColor = Color.Blue;

        // The game board we are showing...
        private Board m_board = null;


        #endregion
    }
}
