class ShipTypeEnum(object):
    '''
    An 'enum' for ship types.
    '''
    CARRIER = "CARRIER"
    BATTLESHIP = "BATTLESHIP"
    MINELAYER = "MINELAYER"


class OrientationEnum(object):
    '''
    An enum for ship orientations.
    '''
    HORIZONTAL = "HORIZONTAL"
    VERTICAL = "VERTICAL"


class BoardSquareCoordinates(object):
    '''
    Coordinates for one square of the board.
    NOTE: Coordinates are 1-based.
    '''
    def __init__(self):
        self.X = 0
        self.Y = 0


class ShipPlacement(object):
    '''
    Holds the position of a ship and its type (eg, CARRIER, BATTLESHIP etc).
    This is used when placing ships on the board and also when movement updates are handled.
    '''
    def __init__(self):
        self.ShipType = ShipTypeEnum.CARRIER
        self.TopLeft = BoardSquareCoordinates()
        self.Orientation = OrientationEnum.HORIZONTAL