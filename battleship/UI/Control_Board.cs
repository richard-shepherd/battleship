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
            set { setBoardSize(value); Invalidate(); }
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

            // We make sure that the board size is set up...
            setBoardSize(board.BoardSize);

            // We draw the board...
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
                e.Graphics.Clear(Color.FromArgb(0xff, 0xf8, 0xff, 0xff));

                // We draw the grid...
                drawGrid(e.Graphics);

                // We draw the items on the board...
                drawBoard(e.Graphics);
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
        public void drawBoard(Graphics graphics)
        {
            if(m_board != null)
            {
                drawShips(graphics);
                drawShells(graphics);
                drawMines(graphics);
                drawDrones(graphics);
            }
        }

        /// <summary>
        /// Draws each ship on the board.
        /// </summary>
        private void drawShips(Graphics graphics)
        {
            var brush_undamaged = new SolidBrush(m_playerColor);
            var brush_damaged = new SolidBrush(Color.Red);

            // We show each ship-part for each ship...
            foreach (var ship in m_board.Ships)
            {
                foreach(var shipPart in ship.ShipParts)
                {
                    // We color the square for the ship part...
                    var x = m_gridSizeX * (shipPart.BoardPosition.X - 1);
                    var y = m_gridSizeY * (shipPart.BoardPosition.Y - 1);
                    var brush = shipPart.IsDamaged ? brush_damaged : brush_undamaged;
                    graphics.FillRectangle(brush, x, y, m_gridSizeX, m_gridSizeY);
                }
            }
        }

        /// <summary>
        /// Draws each shell fired in the most recent turn.
        /// </summary>
        private void drawShells(Graphics graphics)
        {
            var pen = new Pen(Color.Black, 2f);
            var xOffset = m_gridSizeX / 4;
            var yOffset = m_gridSizeY / 4;
            var xLength = xOffset * 2;
            var yLength = yOffset * 2;
            foreach (var shelledSquare in m_board.ShelledSquares)
            {
                var x1 = m_gridSizeX * (shelledSquare.X - 1) + xOffset;
                var y1 = m_gridSizeY * (shelledSquare.Y - 1) + yOffset;
                var x2 = x1 + xLength;
                var y2 = y1 + yLength;
                graphics.DrawLine(pen, x1, y1, x2, y2);
                graphics.DrawLine(pen, x1, y2, x2, y1);
            }
        }

        /// <summary>
        /// Draws each mine on the board.
        /// </summary>
        private void drawMines(Graphics graphics)
        {
            foreach (var mine in m_board.Mines)
            {
                // We set the transparency of the mine depending on its remaining lifetime...
                var alpha = mine.TurnsRemaining * 255 / Mine.Lifetime;
                var color = Color.FromArgb(alpha, Color.Purple);
                var brush = new SolidBrush(color);

                var x = m_gridSizeX * (mine.BoardPosition.X - 1);
                var y = m_gridSizeY * (mine.BoardPosition.Y - 1);
                graphics.FillEllipse(brush, x, y, m_gridSizeX, m_gridSizeY);
            }
        }

        /// <summary>
        /// Draws each drone on the board.
        /// </summary>
        private void drawDrones(Graphics graphics)
        {
            foreach(var drone in m_board.Drones)
            {
                // We set the transparency of the drone depending on its remaining lifetime...
                var alpha = drone.TurnsRemaining * 255 / Mine.Lifetime;
                var droneColor = Color.FromArgb(alpha, Color.Orange);
                var brush = new SolidBrush(droneColor);

                // We draw a circle for the drone...
                var x = m_gridSizeX * (drone.BoardPosition.X - 1);
                var y = m_gridSizeY * (drone.BoardPosition.Y - 1);
                graphics.FillEllipse(brush, x, y, m_gridSizeX, m_gridSizeY);

                // We draw lines indicating what the drone can see...
                var radarColor = Color.FromArgb(alpha / 4, Color.Orange);
                var pen = new Pen(radarColor);
                x += m_gridSizeX / 2;
                y += m_gridSizeY / 2;
                graphics.DrawLine(pen, x, 0, x, Height - 1);
                graphics.DrawLine(pen, 0, y, Width - 1, y);
            }
        }

        /// <summary>
        /// Sets the board size and redraws the grid.
        /// </summary>
        private void setBoardSize(int boardSize)
        {
            if(boardSize != m_boardSize)
            {
                m_boardSize = boardSize;
                m_fBoardSize = boardSize;
                updateGridSize();
            }
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
