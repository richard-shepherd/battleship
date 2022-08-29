class StartGameResponse(object):
    '''
    Response from AIs to the START_GAME notification.
    The AI tells the game where where to place ships for the start of the game.
    '''
    def __init__(self):
        self.EventName = "START_GAME"
        self.ShipPlacements = []