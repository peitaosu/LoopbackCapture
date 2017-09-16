# Loopback Capture

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/peitaosu/LoopbackCapture/master/LICENSE)

## What is Loopback Capture

Loopback Capture is a tool which can be used to capture the loopback from audio devices.

## Build

The C# version require some packages, before you build it, you need to install these packages:

* CSCore - An advanced audio library, written in C#.
* Unmanaged Exports - DllExport for .Net.

Suggest to use NuGet to install them.

## Usage

```
# CSharp

> LoopbackCapture.exe <output/wav> <time/milliseconds>

# C++

> LoopbackCapture.exe -?
> LoopbackCapture.exe --list-devices
> LoopbackCapture.exe [--device "Device long name"] [--file "file name"] [--time "capture milliseconds"] [--int-16]
    -? prints this message.
    --list-devices displays the long names of all active playback devices.
    --device captures from the specified device (default if omitted)
    --file saves the output to a file
    --time capture specific milliseconds
    --int-16 attempts to coerce data to 16-bit integer format
```

