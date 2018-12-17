import os, sys, subprocess

def record_sounds(output_file="record.wav", time=0):
    if "LOOPBACK_CAPTURE" not in os.environ:
        print("Please set the %LOOPBACK_CAPTURE% before you start to capture.")
        sys.exit(-1)
    if not os.path.isfile(os.environ["LOOPBACK_CAPTURE"]):
        print("File Not Found. Please make sure the %LOOPBACK_CAPTURE% is correct.")
        sys.exit(-1)
    Loopback_Capture_Path = os.environ["LOOPBACK_CAPTURE"]
    if time is not 0:
        process = subprocess.Popen("{} {} {}".format(
            Loopback_Capture_Path, output_file, time), stdout=subprocess.PIPE)
        exit_code = process.wait()
    else:
        process = subprocess.Popen("{} {}".format(
            Loopback_Capture_Path, output_file), stdout=subprocess.PIPE)
        exit_code = process.wait()
    return exit_code
