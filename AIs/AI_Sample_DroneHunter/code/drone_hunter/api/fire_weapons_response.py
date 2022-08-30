from .shared import ShotTypeEnum
from.shared import BoardSquareCoordinates

class FireWeaponsResponse(object):
    '''
    Sent by the AI in response to the FIRE_WEAPONS message.
    '''
    class Shot(object):
        def __init__(self):
            self.ShotType = ShotTypeEnum.SHELL
            self.TargetSquare = BoardSquareCoordinates()

    def __init__(self):
        self.EventName = "FIRE_WEAPONS"
        self.Shots = []
