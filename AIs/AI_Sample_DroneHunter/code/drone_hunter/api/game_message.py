import json

class GameMessage(object):
    '''
    Holds a message from the game engine, decoded from JSON.
    '''
    @staticmethod
    def from_json(json_message):
        '''
        Returns a GameMessage deserialized from the JSON message passed in.
        '''
        return json.loads(json_message, object_hook=GameMessage.from_dict)

    @classmethod
    def from_dict(cls, dict):
        '''
        Sets the data in the class from the dict dserived from the JSON.
        '''
        obj = cls()
        obj.__dict__.update(dict)
        return obj