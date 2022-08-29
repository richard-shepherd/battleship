import sys
import json
from .game_message import GameMessage
from ..api import *

class DroneHunter(object):
    '''
    AI to play Battleship.

    This AI uses drones to find enemy ships and then fires shells based
    on the drone reports. It does not use mines.
    '''
    
    def __init__(self, logger):
        '''
        Constructor.
        '''
        self.logger = logger
        self.shutdown = False  # Set to true when we receive the SHUTDOWN message from the game engine

    def send_message(self, message_object):
        '''
        Serializes the message-object to JSON and sends it to the game engine.
        '''
        self.logger.log("JSON!")
        json_string = json.dumps(vars(message_object))
        self.logger.log(f"TX -> {json_string}")
        print(json_string)

    def play_game(self):
        '''
        Plays the game loop:
        - Listening for messages from the game-engine
        - Processing the messages to produce game actions
        - Sends responses to the game-engine
        '''
        while not self.shutdown:
            try:
                # We wait for a message from the game-engine and log it...
                message = input()
                self.logger.log(f"RX <- {message}")

                # We parse the JSON message and process it depending on the event...
                game_message = GameMessage.from_json(message)
                match game_message.EventName:
                    case "START_GAME":
                        self.on_start_game(game_message)

                    case "SHUTDOWN":
                        self.on_shutdown()
            except Exception as ex:
                self.logger.log(f"Exception: {repr(ex)}")

    def on_start_game(self, start_game_message):
        '''
        Called when we receive the START_GAME message.
        '''
        response = StartGameResponse()
        ship_placement = ShipPlacement()
        #ship_placement.ShipType = ShipTypeEnum.BATTLESHIP
        #ship_placement.Orientation = OrientationEnum.HORIZONTAL
        #ship_placement.TopLeft.X = 20
        #ship_placement.TopLeft.Y = 30
        response.ShipPlacements.append(ship_placement)
        self.send_message(response)

    def on_shutdown(self):
        '''
        Called when we receive the SHUTDOWN message.
        '''
        pass

