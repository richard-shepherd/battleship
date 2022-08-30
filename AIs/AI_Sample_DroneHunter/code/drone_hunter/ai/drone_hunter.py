from http.client import responses
import json
import random
from stat import FILE_ATTRIBUTE_REPARSE_POINT
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
        self.shutdown = False  
        self.start_game_message = None
        self.status_update = None

    def send_message(self, message_object):
        '''
        Serializes the message-object to JSON and sends it to the game engine.
        '''
        json_string = json.dumps(vars(message_object), default=lambda o: vars(o))
        self.logger.log(f"TX<-{json_string}")
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
                self.logger.log(f"RX->{message}")

                # We parse the JSON message and process it depending on the event...
                game_message = GameMessage.from_json(message)
                match game_message.EventName:
                    case "START_GAME":
                        self.on_start_game(game_message)

                    case "FIRE_WEAPONS":
                        self.on_fire_weapons(game_message)

                    case "STATUS_UPDATE":
                        self.on_status_update(game_message)

                    case "MOVE":
                        self.on_move(game_message)

                    case "SHUTDOWN":
                        self.on_shutdown()

            except EOFError:
                # We can receive EOFError from the call to input() if the AI is shut down 
                # without have received or correctly responded to the SHUTDOWN message. If 
                # so, we exit the game loop...
                self.shutdown = True

            except Exception as ex:
                # We log the exception...
                self.logger.log(f"Exception: {repr(ex)}")

    def on_start_game(self, start_game_message):
        '''
        Called when we receive the START_GAME message.
        '''
        # We store the start-game message in case we need its info later.
        # For example, it includes the board size...
        self.start_game_message = start_game_message

        # We create horizontal ships on separate rows at a random column,
        # to avoid them overlapping. For drone-hunting we need to place carriers
        # to launch drones and battleships to fire shells...
        response = StartGameResponse()
        ship_squares_remaining = start_game_message.ShipSquares
        approx_num_ships = start_game_message.ShipSquares / 5
        row_step = int((start_game_message.BoardSize.Y - 2) / approx_num_ships)
        if row_step < 1: row_step = 1
        row = 1
        while ship_squares_remaining >= 5:
            # Carrier...
            if ship_squares_remaining >= 5:
                ship_placement = ShipPlacement()
                ship_placement.ShipType = ShipTypeEnum.CARRIER
                ship_placement.Orientation = OrientationEnum.HORIZONTAL
                ship_placement.TopLeft.X = random.randint(1, start_game_message.BoardSize.X - ShipSizes.CARRIER)
                ship_placement.TopLeft.Y = row
                response.ShipPlacements.append(ship_placement)
                ship_squares_remaining -= ShipSizes.CARRIER
                row += row_step

            # Battleship...
            if ship_squares_remaining >= 4:
                ship_placement = ShipPlacement()
                ship_placement.ShipType = ShipTypeEnum.BATTLESHIP
                ship_placement.Orientation = OrientationEnum.HORIZONTAL
                ship_placement.TopLeft.X = random.randint(1, start_game_message.BoardSize.X - ShipSizes.BATTLESHIP)
                ship_placement.TopLeft.Y = row
                response.ShipPlacements.append(ship_placement)
                ship_squares_remaining -= ShipSizes.BATTLESHIP
                row += row_step

        # We send the response to the game engine...
        self.send_message(response)

    def on_fire_weapons(self, fire_weapons_message):
        '''
        Called when we receive the FIRE_WEAPONS message.
        '''
        response = FireWeaponsResponse()

        # We launch drones to radom squares...
        for i in range(fire_weapons_message.AvailableDrones):
            shot = FireWeaponsResponse.Shot()
            shot.ShotType = ShotTypeEnum.DRONE
            shot.TargetSquare.X = random.randint(1, self.start_game_message.BoardSize.X)
            shot.TargetSquare.Y = random.randint(1, self.start_game_message.BoardSize.Y)
            response.Shots.append(shot)

        if self.status_update is None or not self.status_update.DroneReports:
            # We do not (yet) have any information from drones, so we fire shells randomly...
            self.fire_randomly(response, fire_weapons_message.AvailableShells)
        else:
            # We have info from drones, so we target based on the drone information...
            self.fire_using_drone_report(response, fire_weapons_message.AvailableShells)

        # We send the response to the game engine...
        self.send_message(response)

    def fire_using_drone_report(self, response, num_shells):
        '''
        Fires shells based on the most recent drone report.
        '''
        # This AI only looks at information from the first drone to report, and 
        # only fires in the first direction reported by this drone...
        drone_report = self.status_update.DroneReports[0]
        direction = drone_report.DirectionsDetected[0]

        # We fire random shells on the row or column where the drone is located
        # in the direction detected...
        drone_x = drone_report.DronePosition.X
        drone_y = drone_report.DronePosition.Y
        for i in range(num_shells):
            shot = FireWeaponsResponse.Shot()
            shot.ShotType = ShotTypeEnum.SHELL

            match direction:
                case "NORTH":
                    shot.TargetSquare.X = drone_x
                    shot.TargetSquare.Y = random.randint(1, drone_y - 1)

                case "SOUTH":
                    shot.TargetSquare.X = drone_x
                    shot.TargetSquare.Y = random.randint(drone_y + 1, self.start_game_message.BoardSize.Y)

                case "EAST":
                    shot.TargetSquare.X = random.randint(drone_x + 1, self.start_game_message.BoardSize.X)
                    shot.TargetSquare.Y = drone_y

                case "WEST":
                    shot.TargetSquare.X = random.randint(1, drone_x - 1)
                    shot.TargetSquare.Y = drone_y

                case "OVER_SHIP":
                    shot.TargetSquare.X = drone_x
                    shot.TargetSquare.Y = drone_y

            response.Shots.append(shot)

    def fire_randomly(self, response, num_shells):
        '''
        Fires shells randomly.
        '''
        for i in range(num_shells):
            shot = FireWeaponsResponse.Shot()
            shot.ShotType = ShotTypeEnum.SHELL
            shot.TargetSquare.X = random.randint(1, self.start_game_message.BoardSize.X)
            shot.TargetSquare.Y = random.randint(1, self.start_game_message.BoardSize.Y)
            response.Shots.append(shot)

    def on_status_update(self, status_update_message):
        '''
        Called when we receive the STATUS_UPDATE message.
        '''
        # We store the status update, and ACK that we have received it...
        self.status_update = status_update_message
        self.send_message(StatusUpdateResponse())

    def on_move(self, move_message):
        '''
        Called when we receive the MOVE message.
        '''
        # This AI does not move ships, so we send an empty response...
        self.send_message(MoveResponse())

    def on_shutdown(self):
        '''
        Called when we receive the SHUTDOWN message.
        '''
        # We ACK that we have received this message, and note that we should shut down...
        self.send_message(ShutdownResponse())
        self.shutdown = True

