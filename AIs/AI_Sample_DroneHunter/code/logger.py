import os
from pathlib import Path

class Logger(object):
    '''
    Writes log messages to a file.
    '''
    def __init__(self):
        '''
        Constructor.
        '''
        # We create the log folder...
        Path("Logs").mkdir(parents=True, exist_ok=True)

        # We create the log file name...
        pid = os.getpid()
        self.log_filename = f"Logs/DroneHunter_{pid}.log"

    def log(self, message):
        '''
        Write the message to the log file.
        '''
        with open(self.log_filename, "a") as log_file:
            log_file.write(message + "\n\n")
        