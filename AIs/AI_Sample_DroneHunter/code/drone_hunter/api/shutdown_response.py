class ShutdownResponse(object):
    '''
    ACK response from AIs to the SHUTDOWN message.
    '''
    def __init__(self):
        self.EventName = "SHUTDOWN"