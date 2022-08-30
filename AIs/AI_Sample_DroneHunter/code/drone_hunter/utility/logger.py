import os
import time
from datetime import datetime
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
        folder = "Logs"
        Path(folder).mkdir(parents=True, exist_ok=True)

        # We delete any old log files...
        cutoff = time.time() - 60
        for f in os.listdir(folder):
            f = os.path.join(folder, f)
            if os.stat(f).st_mtime < cutoff:
                if os.path.isfile(f):
                    os.remove(f)

        # We create the log file name...
        pid = os.getpid()
        self.log_filename = f"{folder}/DroneHunter_{pid}.log"

    def log(self, message):
        '''
        Writes a message to the log file.
        '''
        time_string = datetime.now().strftime("%H:%M:%S.%f")[:-3]
        with open(self.log_filename, "a") as log_file:
            log_file.write(f"{time_string}: {message}"+ "\n\n")
        