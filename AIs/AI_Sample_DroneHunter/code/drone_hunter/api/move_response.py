from .shared import ShipPlacement

class MoveResponse(object):
    '''
    Sent by AIs in response to the MOVE message.
    '''
    class MovementRequest(object):
        def __init__(self):
            self.Index = 0;
            self.ShipPlacement = ShipPlacement()

    def __init__(self):
        self.EventName = "MOVE"
        self.MovementRequests = []

