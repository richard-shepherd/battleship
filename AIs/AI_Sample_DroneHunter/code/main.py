import sys
from drone_hunter import DroneHunter
from logger import Logger

# We create the logger, and log the Python version we are running ...
logger = Logger()
logger.log(f"Starting DroneHunter AI. Python={sys.version}")

# We create the drone-hunter AI...
drone_hunter = DroneHunter(logger)
drone_hunter.play_game()
