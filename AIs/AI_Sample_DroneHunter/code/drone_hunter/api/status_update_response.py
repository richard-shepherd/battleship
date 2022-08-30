class StatusUpdateResponse(object):
    '''
    Response ACK from AIs to the STATUS_UPDATE message.
    '''
    def __init__(self):
        self.EventName = "STATUS_UPDATE"